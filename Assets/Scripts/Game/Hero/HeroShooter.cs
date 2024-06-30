﻿using Game.Common.Damage;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    public class HeroShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint = null!;
        [SerializeField] private HeroBullet _bulletPrefab = null!;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _bulletSpeed;
        [SerializeField, Min(1)] private int _bulletDamage;

        private float _currentCooldown;

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;
        }

        public void TryShoot()
        {
            if(_currentCooldown > 0)
                return;
            
            _currentCooldown = _shootCooldown;
            
            var bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.Construct(GetDirection(), _bulletSpeed, new Damage(_bulletDamage));
        }

        private Direction GetDirection() => 
            transform.lossyScale.x > 0 ? Direction.Right : Direction.Left;
    }
}