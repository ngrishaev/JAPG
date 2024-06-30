using System;
using System.Collections;
using Game.Common;
using Game.Common.Damage;
using Game.Common.Health;
using Game.Shared.HorizontalMover;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Enemy
{
    public class Slime : MonoBehaviour, IDamageable, IStunnable
    {
        [SerializeField] private HeroHurtboxDetector _hurtboxDetector = null!;
        [SerializeField] private HorizontalPeriodicMover _mover = null!;
        [SerializeField] private float _speed;
        [SerializeField] private int _healthStartValue;
        
        private Health _health = null!;

        private void Awake()
        {
            _health = new Health(_healthStartValue);
            
            _hurtboxDetector.OnHeroTriggered += OnHeroHit;
            _mover.OnDirectionChanged += OnMoveDirectionChanged;
            
            _mover.Construct(_speed);
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
                Destroy(gameObject);
            }
        }

        public void Stun(float duration)
        {
            StartCoroutine(StunRoutine(duration));
        }

        private IEnumerator StunRoutine(float duration)
        {
            _mover.Stop();
            yield return new WaitForSeconds(duration);
            _mover.Start();
        }
    }
}
