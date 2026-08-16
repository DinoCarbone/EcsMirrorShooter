using ECS.Common.Cursor.Interfaces;
using UnityEngine;

namespace ECS.Common.Cursor.Services
{
    public class UnityPlayerCursorService : IPlayerCursorSystem
    {
        public UnityPlayerCursorService()
        {
            Show();
        }

        public void Hide()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }

        public void Show()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
