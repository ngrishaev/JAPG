using Game.Common.Damage;
using Infrastructure.Services.StaticData;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero.Attack
{
    public class RocketShooter : ProjectileShooter
    {
        private readonly HeroRocket _prefab;
        private readonly HeroRocketStaticData _staticData;

        public RocketShooter(CooldownChecker cooldown, Transform shootPoint, HeroRocket prefab, HeroRocketStaticData staticData) : base(cooldown, shootPoint)
        {
            _prefab = prefab;
            _staticData = staticData;
        }

        protected override void Shoot(Direction direction, Vector3 shootPointPosition)
        {
            var rocket = Object.Instantiate(_prefab, shootPointPosition, Quaternion.identity);
            rocket.Construct(direction, _staticData.Speed, new Damage(_staticData.Damage));
        }
    }
}