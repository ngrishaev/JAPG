using System;
using Services.Input;
using UnityEngine;

namespace Game.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2 = null!;
        [SerializeField] private HeroAnimations _animations = null!;
        [SerializeField] private GroundDetector _groundDetector = null!;
        [SerializeField] private float _jumpForce;
        
        private IInput _input = null!;

        private void Awake()
        {
            _input = Infrastructure.Game.Input;
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            HandleJump();
        }

        private void HandleJump()
        {
            if (!_input.JumpPressed)
                return;
            if (!_groundDetector.CheckIsGrounded())
                return;
        
            _rigidbody2.AddForce(Vector2.up * _jumpForce);
        }
    }
}
