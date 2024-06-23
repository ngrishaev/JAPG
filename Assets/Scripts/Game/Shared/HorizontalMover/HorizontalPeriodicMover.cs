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
        
        public event Action<Direction> OnDirectionChanged = null!;
        private float _speed;

        public void Construct(float speed)
        {
            _speed = transform.localScale.x * speed;
            
            _leftEdge.OnEdgeReached += OnEdgeReached;
            _rightEdge.OnEdgeReached += OnEdgeReached;
        }

        private void FixedUpdate()
        {
            _body.velocity = new Vector2(_speed, _body.velocity.y);
        }

        private void OnEdgeReached()
        {
            _speed *= -1;
            OnDirectionChanged?.Invoke(_speed > 0 ? Direction.Right : Direction.Left);
        }
    }
}
