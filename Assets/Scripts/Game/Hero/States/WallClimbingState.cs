using UnityEngine;

namespace Game.Hero.States
{
    public class WallClimbingState : IHeroState
    {
        private readonly Rigidbody2D _playerBody;
        public string Name => "WallClimbing";

        public WallClimbingState(Rigidbody2D playerBody)
        {
            _playerBody = playerBody;
        }

        public void Enter()
        {
            _playerBody.simulated = false;
        }

        public void Exit()
        {
            _playerBody.simulated = true;
        }

        public void Update(float deltaTime)
        {
        }
    }
}