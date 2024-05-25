using UnityEngine;

namespace Services.Input
{
    public class KeyboardInput : IInput
    {
        public float HorizontalMovement => UnityEngine.Input.GetAxisRaw("Horizontal");
        public bool Jump => UnityEngine.Input.GetKeyDown(KeyCode.Space);
    }
}