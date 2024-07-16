using Tools.Enums;
using UnityEngine;

namespace Game.Hero.Attack
{
    public abstract class ProjectileShooter
    {
        private readonly CooldownChecker _cooldown;
        private readonly Transform _shootPoint;

        protected ProjectileShooter(CooldownChecker cooldown, Transform shootPoint)
        {
            _cooldown = cooldown;
            _shootPoint = shootPoint;
        }

        public void TryShoot(Direction direction)
        {
            if(!_cooldown.IsCooldownPassed())
                return;
            
            _cooldown.ResetCooldown();
            
            Shoot(direction, _shootPoint.position);
        }

        protected abstract void Shoot(Direction direction, Vector3 shootPointPosition);
    }
}