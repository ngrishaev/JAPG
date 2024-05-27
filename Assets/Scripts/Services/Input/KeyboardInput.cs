using UnityEngine;

namespace Services.Input
{
    public class KeyboardInput : IInput
    {
        public float HorizontalMovement => UnityEngine.Input.GetAxisRaw("Horizontal");
        public bool JumpPressed => UnityEngine.Input.GetKeyDown(KeyCode.Space);
    }
}