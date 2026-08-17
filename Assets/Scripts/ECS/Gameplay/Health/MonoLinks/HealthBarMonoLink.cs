using ECS.Common.MonoLinksBase;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace ECS.Gameplay.Health.MonoLinks
{
    public class HealthBarMonoLink : MonoLink<HealthBarComponent>, IHealthBar
    {
        [SerializeField] private Image image;

        public override void Make(ref EcsEntity entity)
        {
            Value.Value = this;
            base.Make(ref entity);
        }

        public void SetValue(float normalizedValue)
        {
            image.fillAmount = normalizedValue;
        }
    }
}
