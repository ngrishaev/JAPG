using Game.Common.Damage;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    [RequireComponent(typeof(Collider2D))]
    public class HeroRocket : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null!;
        private Damage _explosionDamage = null!;

        // TODO: extract projectile class?
        public void Construct(Direction direction, float speed, Damage damage)
        {
            _explosionDamage = damage;
            transform.localScale = direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
            
            var speedAlongDirection = direction == Direction.Right ? speed : -speed;
            _body.velocity = new Vector2(speedAlongDirection, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var effectedByExplosion = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.5f, LayerMask.GetMask("EnemyHurtbox"));
            foreach (var obj in effectedByExplosion)
            {
                var damageable = obj.GetComponentInParent<IDamageable>();
                damageable?.ReceiveDamage(_explosionDamage);
            }

            Destroy(gameObject);
        }
    }
}