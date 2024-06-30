using System;
using Tools.Enums;
using UnityEngine;

namespace Game.Shared.HorizontalMover
{
    public class HorizontalPeriodicMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null!;
        [SerializeField] private HorizontalMoverEdge _leftEdge = null!;
        [SerializeField] private HorizontalMoverEdge _rightEdge = null!;
        
        public event Action<Direction>? OnDirectionChanged;
        private float _currentSpeed;
        private float _moveSpeed;

        public void Construct(float speed)
        {
            _moveSpeed = speed;
            Start(speed);
            
            _leftEdge.OnEdgeReached += OnEdgeReached;
            _rightEdge.OnEdgeReached += OnEdgeReached;
        }

        private void Start(float speed)
        {
            _currentSpeed = transform.localScale.x * speed;
        }

        private void FixedUpdate()
        {
            _body.velocity = new Vector2(_currentSpeed, _body.velocity.y);
        }

        private void OnEdgeReached()
        {
            _currentSpeed *= -1;
            OnDirectionChanged?.Invoke(_currentSpeed > 0 ? Direction.Right : Direction.Left);
        }
        
        public void Start()
        {
            Start(_moveSpeed);
        }

        public void Stop()
        {
            _currentSpeed = 0;
        }
    }
}
