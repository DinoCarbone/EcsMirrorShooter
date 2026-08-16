using ECS.Gameplay.Cursor.Interfaces;
using UnityEngine;

namespace ECS.Gameplay.Cursor.Services
{
    public class UnityCursorController : ICursorController
    {
        public void LockAndHide()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }

        public void UnlockAndShow()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
