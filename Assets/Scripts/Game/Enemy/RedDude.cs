using UnityEngine;

namespace Game.Enemy
{
    public class RedDude : MonoBehaviour
    {
        [SerializeField] private Transform _bulletsSpawnPoint = null!;
        [SerializeField] private Transform _transform = null!;
        [SerializeField] private Bullet _bulletPrefab = null!;
        [SerializeField] private float _shootCooldownTime = 2f;
        [SerializeField] private float _shootCooldownTimer;
        
        private GameObject _hero = null!;

        public void Construct(GameObject hero)
        {
            _hero = hero;
        }

        private void Update()
        {
            RotateToTarget(_hero);
            ProcessShootForward();
        }

        private void ProcessShootForward()
        {
            UpdateShootCooldown();
            TryShoot();
        }

        private void TryShoot()
        {
            if(_hero == null)
                return;
            
            if (_shootCooldownTimer > 0)
                return;
            
            var bullet = Instantiate(_bulletPrefab, _bulletsSpawnPoint.position, Quaternion.identity);
            bullet.SetDirection(
                _bulletsSpawnPoint.position.x > _transform.position.x
                    ? Vector2.right
                    : Vector2.left);
            _shootCooldownTimer = _shootCooldownTime;
        }

        private void UpdateShootCooldown()
        {
            if (_shootCooldownTimer <= 0)
                return;
            
            _shootCooldownTimer -= Time.deltaTime;
        }

        // Extract this method into separate component
        private void RotateToTarget(GameObject? hero)
        {
            if (hero == null) 
                return;

            transform.localScale = hero.transform.position.x < transform.position.x
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }
    }
}
