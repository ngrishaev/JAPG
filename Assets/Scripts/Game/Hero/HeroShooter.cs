using UnityEngine;

namespace Game.Hero
{
    public class HeroShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint = null!;
        [SerializeField] private HeroBullet _bulletPrefab = null!;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _shootCooldown;
        
        private float _currentCooldown;
        
        public void TryShoot()
        {
            var bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            var bulletSpeedWithDirection = transform.lossyScale.x > 0 ? _bulletSpeed : -_bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeedWithDirection, 0);
            _currentCooldown = _shootCooldown;
        }
        
        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;
        }
    }
}