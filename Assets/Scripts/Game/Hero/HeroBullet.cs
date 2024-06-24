using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Hero
{
    [RequireComponent(typeof(Collider2D))]
    public class HeroBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null!;
        
        

        public void Construct(Direction direction, float bulletSpeed)
        {
            transform.localScale = direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
            
            var speedAlongDirection = direction == Direction.Right ? bulletSpeed : -bulletSpeed;
            _body.velocity = new Vector2(speedAlongDirection, 0);
        }
    }
}
