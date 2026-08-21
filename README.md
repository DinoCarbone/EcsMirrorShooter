# ECS Shooter

A compact third-person shooter and architecture sample built with **Unity**, **LeoECS**, **Mirror**, and **Zenject**. The project demonstrates how to keep gameplay data-oriented while isolating networking, presentation, and Unity-specific behaviour behind clear boundaries.

## ECS and Mirror

Each client creates an ECS entity only for its local player. `MirrorEcsEntityInitializer` checks `isLocalPlayer`, so input, movement, aiming, shooting, and other gameplay systems never run for remote players on that client. Remote players remain lightweight Mirror-driven Unity representations synchronized through `NetworkTransform` and `NetworkAnimator`.

Network-aware operations follow a consistent flow:

```text
Input → ECS systems → service interface → Mirror decorator → server/client
```

Core services implement local gameplay behaviour. Mirror decorators add message routing, ownership checks, network spawning, and destruction without leaking Mirror APIs into ECS systems. The same gameplay code can therefore run in both offline and online scenes.

## Architecture

| Layer | Responsibility |
|---|---|
| `ECS/Gameplay` | Movement, jumping, aiming, shooting, damage, health, death, and animation |
| `ECS/Common` | Camera, UI, input state, collision bridge, and object lifecycle |
| `Networking/Mirror` | Network decorators, message handlers, ownership, and prefab registration |
| `DI` | Composition root and online/offline configuration |

Prefabs are connected to ECS through `MonoEntity` and typed `MonoLink<T>` components. Components contain state, systems contain behaviour, and transient actions use one-frame signals such as `SpawnBulletSignal`, `DamageSignal`, `HealthChangedSignal`, and `CollisionSignal`.

System order is explicit in `EcsStartup`. Frame logic and physics logic use separate `Tick` and `FixedTick` pipelines, making execution order easy to inspect and extend.

## Design highlights

- Feature-oriented subsystems with dedicated `Components`, `Systems`, `Services`, `Interfaces`, and `MonoLinks`.
- Local-player-only ECS simulation prevents duplicate input and gameplay execution for remote characters.
- Mirror integration is implemented through decorators instead of network-specific gameplay systems.
- Network prefabs are instantiated through Zenject, preserving dependency injection after Mirror spawning.
- One-frame signals make gameplay event chains explicit and automatically remove temporary state.
- Camera, HUD, menu, cursor, spawning, and lifecycle logic use the same interface-driven approach as gameplay services.
- Offline and online modes share the same ECS pipeline and differ only in composition.

## Features

- Rigidbody-based movement and jumping;
- mouse aiming, Cinemachine camera, and Animation Rigging;
- network projectile spawning and synchronization;
- collision and trigger damage;
- health HUD, death, and respawn flow;
- synchronized transforms and animations;
- pause menu with player input blocking.

## Scenes

- `Assets/Scenes/Offline.unity` — standalone gameplay; the local player is created directly through `AutomaticSpawnInstaller`.
- `Assets/Scenes/Online.unity` — Mirror host/client gameplay with network spawning and service decorators.

Open the desired scene and enter Play Mode. In the online scene, use `NetworkManagerHUD` to start a Host or Client; a second instance can be launched through ParrelSync or a standalone build.

Controls: `WASD` — move, `Space` — jump, mouse — look and aim, `LMB` — shoot, `Esc` — menu.
