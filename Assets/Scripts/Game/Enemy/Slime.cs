using System.Collections;
using Game.Common.Damage;
using Game.Common.Health;
using Game.Shared.HorizontalMover;
using Infrastructure.Services.Reset;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Enemy
{
    public class Slime : MonoBehaviour, IDamageable, IStunnable, IResetable
    {
        [SerializeField] private HeroHurtboxDetector _hurtboxDetector = null!;
        [SerializeField] private HorizontalPeriodicMover _mover = null!;
        
        private Health _health = null!;
        private ResetData _resetData = null!;

        public void Construct(float speed, int health)
        {
            _hurtboxDetector.OnHeroTriggered += OnHeroHit;
            _mover.OnDirectionChanged += OnMoveDirectionChanged;
            
            _resetData = new ResetData(
                transform.position,
                transform.localScale.x > 0 ? Direction.Right : Direction.Left,
                health);
            
            _health = new Health(health); // 3
            _mover.Construct(speed); // 2
        }

        private void OnMoveDirectionChanged(Direction direction)
        {
            transform.localScale = direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
        }

        private void OnHeroHit(Hero.Hero hero)
        {
            hero.Damage();
        }

        public void ReceiveDamage(Damage damage)
        {
            _health.ReceiveDamage(damage);
            if (_health.IsDead())
            {
                gameObject.SetActive(false);
            }
        }

        public void Stun(float duration)
        {
            StartCoroutine(StunRoutine(duration));
        }

        private IEnumerator StunRoutine(float duration)
        {
            _mover.StopMoving();
            yield return new WaitForSeconds(duration);
            _mover.StartMoving();
        }

        public void Reset()
        {
            transform.position = _resetData.Position;
            transform.localScale = _resetData.Direction == Direction.Right 
                ? transform.localScale.WithX(1) 
                : transform.localScale.WithX(-1);
            _health = new Health(_resetData.Health);
            gameObject.SetActive(true);
        }

        private class ResetData
        {
            public Vector3 Position;
            public Direction Direction;
            public int Health;

            public ResetData(Vector3 position, Direction direction, int health)
            {
                Position = position;
                Direction = direction;
                Health = health;
            }
        }
    }
}
