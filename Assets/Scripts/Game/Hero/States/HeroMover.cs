using Services.Input;
using UnityEngine;

namespace Game.Hero.States
{
    public class HeroMover {
        private readonly IInput _input;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        public Direction FacingDirection { get; private set; } = Direction.Right;
        public bool IsMoving => _rigidbody.velocity.x != 0;

        public HeroMover(IInput input, Rigidbody2D rigidbody, float speed)
        {
            _input = input;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void UpdateMovement()
        {
            float horizontalInput = _input.HorizontalMovement;
            _rigidbody.velocity = new Vector2(horizontalInput * _speed, _rigidbody.velocity.y);
            FacingDirection = GetDirection(horizontalInput, FacingDirection);
            FaceByDirection(FacingDirection);
            
        }

        private Direction GetDirection(float horizontalInput, Direction current) =>
            horizontalInput switch
            {
                < 0 => Direction.Left,
                > 0 => Direction.Right,
                _ => current
            };

        private void FaceByDirection(Direction direction) =>
            _rigidbody.transform.localScale = direction switch
            {
                Direction.Left => new Vector3(-1, 1, 1),
                Direction.Right => new Vector3(1, 1, 1),
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, "Unknown direction"),
            };

        public enum Direction
        {
            Left,
            Right,
        }
    }
}