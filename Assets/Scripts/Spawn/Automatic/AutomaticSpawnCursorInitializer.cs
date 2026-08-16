using System;
using ECS.Gameplay.Cursor.Interfaces;
using Zenject;

namespace Spawn.Automatic
{
    public class AutomaticSpawnCursorInitializer : IInitializable, IDisposable
    {
        private readonly ICursorController cursorController;

        public AutomaticSpawnCursorInitializer(ICursorController cursorController)
        {
            this.cursorController = cursorController ??
                throw new ArgumentNullException(nameof(cursorController));
        }

        public void Initialize()
        {
            cursorController.LockAndHide();
        }

        public void Dispose()
        {
            cursorController.UnlockAndShow();
        }
    }
}
