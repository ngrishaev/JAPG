using Game.Data;
using UnityEngine;

namespace Game.Hero.States
{
    // TODO - this is basically dash state copypast, need to refactor
    public class AirDashState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        private readonly HeroDashData _dashData;
        public string Name => "AirDashState";
        private const float TimeLengthSeconds = 0.2f;
        private const float Distance = 3f;
        private float _currentDashTime;

        public AirDashState(Rigidbody2D playerBody, HeroDashData dashData)
        {
            _playerBody = playerBody;
            _dashData = dashData;
        }

        public void Enter()
        {
            _dashData.StartAirDash();
            
            var force = Distance / TimeLengthSeconds;
            _playerBody.velocity = new Vector2(_playerBody.transform.localScale.x * force, 0);
            _currentDashTime = TimeLengthSeconds;
        }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            if(_currentDashTime <= 0)
                return;
            
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, 0);
            _currentDashTime -= deltaTime;
            if (_currentDashTime <= 0) 
                _dashData.EndDash();
        }
    }
}