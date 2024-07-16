using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Game.Hero.States
{
    public class WallJumpState: IHeroState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroStaticData _staticData;
        public string Name => "WallJump";

        public WallJumpState(
            Rigidbody2D rigidbody,
            HeroStaticData staticData
            )
        {
            _rigidbody = rigidbody;
            _staticData = staticData;
        }
        
        public void Enter()
        {
            var jumpDirection = _rigidbody.transform.localScale.x > 0 ? -1 : 1;
            _rigidbody.gravityScale = _staticData.JumpGravity;
            var jumpHeight = _staticData.WallJumpHeight;
            var yVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(_staticData.WallJumpHorizontalForce * jumpDirection, yVelocity);
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
        }
    }
}