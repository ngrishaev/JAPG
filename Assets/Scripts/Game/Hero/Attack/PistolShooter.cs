using Game.Common.Damage;
using Infrastructure.Services.StaticData;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero.Attack
{
    public class PistolShooter : ProjectileShooter
    {
        private readonly HeroBullet _bulletPrefab;
        private readonly HeroPistolStaticData _staticData;

        public PistolShooter(CooldownChecker cooldown, Transform shootPoint, HeroBullet bulletPrefab, HeroPistolStaticData staticData) : base(cooldown, shootPoint)
        {
            _bulletPrefab = bulletPrefab;
            _staticData = staticData;
        }

        protected override void Shoot(Direction direction, Vector3 shootPointPosition)
        {
            var bullet = Object.Instantiate(_bulletPrefab, shootPointPosition, Quaternion.identity);
            bullet.Construct(direction, _staticData.Speed, new Damage(_staticData.Damage));
        }
    }
}