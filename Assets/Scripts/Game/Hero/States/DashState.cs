using Game.Data;
using UnityEngine;

namespace Game.Hero.States
{
    public class DashState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        private readonly HeroDashData _dashData;
        public string Name => "DashState";
        private const float _force = 10f;
        private const float _timeLengthSeconds = 0.2f;
        private float _currentDashTime;

        public DashState(Rigidbody2D playerBody, HeroDashData dashData)
        {
            _playerBody = playerBody;
            _dashData = dashData;
        }

        public void Enter()
        {
            _dashData.StartDash();
            _playerBody.velocity = new Vector2(_playerBody.transform.localScale.x * _force, 0);
            _currentDashTime = _timeLengthSeconds;
        }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            _currentDashTime -= deltaTime;
            if (_currentDashTime <= 0) 
                _dashData.EndDash();
        }
    }
    
    public class AirDashState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        private readonly HeroDashData _dashData;
        public string Name => "AirDashState";
        private const float _force = 10f;
        private const float _timeLengthSeconds = 0.2f;
        private float _currentDashTime;

        public AirDashState(Rigidbody2D playerBody, HeroDashData dashData)
        {
            _playerBody = playerBody;
            _dashData = dashData;
        }

        public void Enter()
        {
            _dashData.StartDash();
            _dashData.SpendAirDash();
            _playerBody.velocity = new Vector2(_playerBody.transform.localScale.x * _force, 0);
            _currentDashTime = _timeLengthSeconds;
        }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            _currentDashTime -= deltaTime;
            if (_currentDashTime <= 0)
            {
                _dashData.EndDash();
                return;
            }
            
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, 0);
        }
    }
}