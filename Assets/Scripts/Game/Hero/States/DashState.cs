using Game.Data;
using UnityEngine;

namespace Game.Hero.States
{
    public class DashState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        private readonly HeroDashData _dashData;
        public string Name => "DashState";
        private float _distance = 3f;
        private float _timeLengthSeconds = 0.5f;
        private Vector2 _endPosition;

        public DashState(Rigidbody2D playerBody, HeroDashData dashData)
        {
            _playerBody = playerBody;
            _dashData = dashData;
        }

        public void Enter()
        {
            _dashData.StartDash();
            
            var obstacle = Physics2D.Raycast(_playerBody.position, _playerBody.transform.right, _distance);
            if (obstacle.collider == null)
            {
                _endPosition = _playerBody.position + (Vector2)_playerBody.transform.right * _distance;
                return;
            }
            
            _endPosition = obstacle.point;
            
            
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            var speed = _distance / _timeLengthSeconds;
            _playerBody.position = Vector2.MoveTowards(_playerBody.position, _endPosition, speed * deltaTime);
            
            if (Mathf.Abs(_playerBody.position.x - _endPosition.x) < 0.001f) 
                _dashData.EndDash();
        }
    }
}