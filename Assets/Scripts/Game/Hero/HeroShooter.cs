using Game.Common.Damage;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    // TODO: obviously need to split this class
    public class HeroShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint = null!;
        [Header("Bullet")]
        [SerializeField] private HeroBullet _bulletPrefab = null!;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _bulletSpeed;
        [SerializeField, Min(1)] private int _bulletDamage;
        [Header("Rocket")]
        [SerializeField] private HeroRocket _rocketPrefab = null!;
        [SerializeField] private float _rocketShootCooldown;
        [SerializeField] private float _rocketSpeed;
        [SerializeField, Min(1)] private int _rocketDamage;

        private float _buleltCooldown;
        private float _rocketCooldown;

        private void Update()
        {
            if (_buleltCooldown > 0)
                _buleltCooldown -= Time.deltaTime;
            
            if (_rocketCooldown > 0)
                _rocketCooldown -= Time.deltaTime;
        }

        public void TryShootBullet()
        {
            if(_buleltCooldown > 0)
                return;
            
            _buleltCooldown = _shootCooldown;
            
            var bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.Construct(GetDirection(), _bulletSpeed, new Damage(_bulletDamage));
        }
        
        public void TryShootRocket()
        {
            if(_rocketCooldown > 0)
                return;
            
            _rocketCooldown = _rocketShootCooldown;
            
            var rocket = Instantiate(_rocketPrefab, _shootPoint.position, Quaternion.identity);
            rocket.Construct(GetDirection(), _rocketSpeed, new Damage(_rocketDamage));
        }

        private Direction GetDirection() => 
            transform.lossyScale.x > 0 ? Direction.Right : Direction.Left;
    }
}