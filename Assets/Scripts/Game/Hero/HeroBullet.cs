using System;
using Game.Common.Damage;
using Game.Enemy;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    [RequireComponent(typeof(Collider2D))]
    public class HeroBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null!;
        private Damage _bulletDamage = null!;

        public void Construct(Direction direction, float bulletSpeed, Damage bulletDamage)
        {
            _bulletDamage = bulletDamage;
            transform.localScale = direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
            
            var speedAlongDirection = direction == Direction.Right ? bulletSpeed : -bulletSpeed;
            _body.velocity = new Vector2(speedAlongDirection, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageable = other.GetComponentInParent<IDamageable>();
            damageable?.ReceiveDamage(_bulletDamage);

            Destroy(gameObject);
        }
    }
}
