using Game.Common.Damage;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    [RequireComponent(typeof(Collider2D))]
    public class HeroStun : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null!;
        private float _stunDuration;

        public void Construct(Direction direction, float stunDuration, float bulletSpeed)
        {
            _stunDuration = stunDuration;
            transform.localScale = direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
            
            var speedAlongDirection = direction == Direction.Right ? bulletSpeed : -bulletSpeed;
            _body.velocity = new Vector2(speedAlongDirection, 0);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageable = other.GetComponentInParent<IStunnable>();
            damageable?.Stun(_stunDuration);

            Destroy(gameObject);
        }
    }
}
