using System;
using ECS.Gameplay.Cursor.Interfaces;

namespace Networking.Mirror.Integration
{
    public class MirrorCursorHandler : IMirrorServerHandler
    {
        private readonly ICursorController cursorController;

        public MirrorCursorHandler(ICursorController cursorController)
        {
            this.cursorController = cursorController ??
                throw new ArgumentNullException(nameof(cursorController));
        }

        public void RegisterHandler()
        {
            cursorController.LockAndHide();
        }

        public void UnregisterHandler()
        {
            cursorController.UnlockAndShow();
        }
    }
}
