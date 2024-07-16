using Infrastructure.Services.StaticData;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero.Attack
{
    public class StunShooter : ProjectileShooter
    {
        private readonly HeroStun _prefab;
        private readonly HeroStunStaticData _staticData;

        public StunShooter(CooldownChecker cooldown, Transform shootPoint, HeroStun prefab, HeroStunStaticData staticData) : base(cooldown, shootPoint)
        {
            _prefab = prefab;
            _staticData = staticData;
        }

        protected override void Shoot(Direction direction, Vector3 shootPointPosition)
        {
            var stun = Object.Instantiate(_prefab, shootPointPosition, Quaternion.identity);
            stun.Construct(direction, _staticData.Duration, _staticData.Speed);
        }
    }
}