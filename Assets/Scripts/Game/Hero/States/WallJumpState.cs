﻿using UnityEngine;

namespace Game.Hero.States
{
    public class WallJumpState: IHeroState
    {
        private readonly Rigidbody2D _rigidbody;
        public string Name => "WallJump";

        public WallJumpState(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        
        public void Enter()
        {
            var jumpDirection = _rigidbody.transform.localScale.x > 0 ? -1 : 1;
            _rigidbody.gravityScale = 3f;
            var jumpHeight = 3f;
            var yVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(4 * jumpDirection, yVelocity);
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
        }
    }
}