using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class KeyboardInput : IInput
    {
        public float HorizontalMovement() => UnityEngine.Input.GetAxisRaw("Horizontal");
        public bool JumpPressedDown() => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public bool JumpPressed() => UnityEngine.Input.GetKey(KeyCode.Space);

        public bool Shoot () =>
            UnityEngine.Input.GetKeyDown(KeyCode.LeftControl) ||
            UnityEngine.Input.GetKeyDown(KeyCode.RightControl);

        public bool RocketShoot() =>
            UnityEngine.Input.GetKeyDown(KeyCode.LeftAlt) ||
            UnityEngine.Input.GetKeyDown(KeyCode.RightAlt);

        public bool PowerShoot() =>
            UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) ||
            UnityEngine.Input.GetKeyDown(KeyCode.RightShift);
    }
}