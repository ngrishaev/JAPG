using UnityEngine;

namespace Game.Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        private Vector2? _direction;

        private void Update()
        {
            MoveForward();
        }
        
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void MoveForward()
        {
            if (!_direction.HasValue)
                return;
            
            transform.position += (Vector3)(_direction.Value * _speed * Time.deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Hero.Hero>(out var hero))
            {
                hero.Damage();
            }
            
            Destroy(gameObject);
        }
    }
}
