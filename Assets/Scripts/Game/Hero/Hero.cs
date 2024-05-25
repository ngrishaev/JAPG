using UnityEngine;

namespace Game.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2 = null!;
        [SerializeField] private GroundDetector _groundDetector = null!;
        [SerializeField] private float _jumpForce;
        
    
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            HandleJump();
            //HandleShoot();
        }

        private void HandleJump()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
                return;
            if (!_groundDetector.CheckIsGrounded())
                return;
        
            _rigidbody2.AddForce(Vector2.up * _jumpForce);
        }
    }
}
