using Services.Input;
using UnityEngine;

namespace Game.Hero.States
{
    public class GroundedState : IHeroState
    {
        private readonly IInput _input;
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroAnimations _heroAnimations;
        private readonly Transform _transform;
        private readonly float _speed;

        public string Name => "GroundedState";

        public GroundedState(
            IInput input,
            Rigidbody2D rigidbody,
            HeroAnimations heroAnimations,
            Transform transform,
            float speed)
        {
            _input = input;
            _rigidbody = rigidbody;
            _heroAnimations = heroAnimations;
            _transform = transform;
            _speed = speed;
        }

        public void Enter()
        {
            
        }

        public void Update(float deltaTime)
        {
            ProcessMovement();
        }

        public void Exit()
        {
            
        }

        private void ProcessMovement()
        {
            float horizontalInput = _input.HorizontalMovement;
            _rigidbody.velocity = new Vector2(horizontalInput * _speed, _rigidbody.velocity.y);

            if (horizontalInput != 0)
                _heroAnimations.PlayRunAnimation();
            else
                _heroAnimations.PlayIdleAnimation();

            UpdateDirection(horizontalInput);
        }


        private void UpdateDirection(float horizontalInput)
        {
            if (horizontalInput < 0)
            {
                _transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontalInput > 0)
            {
                _transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}