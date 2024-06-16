using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class KeyboardInput : IInput
    {
        public float HorizontalMovement => UnityEngine.Input.GetAxisRaw("Horizontal");
        public bool JumpPressedDown => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public bool JumpPressed => UnityEngine.Input.GetKey(KeyCode.Space);
    }
}