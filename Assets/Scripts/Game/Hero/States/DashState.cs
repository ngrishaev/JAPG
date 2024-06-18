﻿using Game.Data;
using UnityEngine;

namespace Game.Hero.States
{
    public class DashState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        private readonly HeroDashData _dashData;
        public string Name => "DashState";
        private const float TimeLengthSeconds = 0.2f;
        private const float Distance = 3f;
        private float _currentDashTime;

        public DashState(Rigidbody2D playerBody, HeroDashData dashData)
        {
            _playerBody = playerBody;
            _dashData = dashData;
        }

        public void Enter()
        {
            _dashData.StartDash();
            
            var force = Distance / TimeLengthSeconds;
            _playerBody.velocity = new Vector2(_playerBody.transform.localScale.x * force, 0);
            _currentDashTime = TimeLengthSeconds;
        }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            _currentDashTime -= deltaTime;
            if (_currentDashTime <= 0) 
                _dashData.EndDash();
        }
    }
}