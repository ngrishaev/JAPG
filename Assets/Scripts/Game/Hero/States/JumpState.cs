﻿using Infrastructure.Services.Input;
using UnityEngine;

namespace Game.Hero.States
{
    public class JumpState : IHeroState
    {
        private readonly IInput _input;
        private readonly HeroMover _heroMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroAnimations _heroAnimations;
        private float _jumpHeight;

        public string Name => "JumpState";

        public JumpState(
            IInput input,
            HeroMover heroMover,
            Rigidbody2D rigidbody,
            HeroAnimations heroAnimations,
            float jumpHeight)
        {
            _input = input;
            _heroMover = heroMover;
            _rigidbody = rigidbody;
            _heroAnimations = heroAnimations;
            _jumpHeight = jumpHeight;
        }

        public void Enter()
        {
            _rigidbody.gravityScale = 3f;
            var jumpSpeed = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _heroAnimations.PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            if(!_input.JumpPressed())
                _rigidbody.gravityScale = 5f;
            _heroMover.UpdateMovement();
        }
    }
}