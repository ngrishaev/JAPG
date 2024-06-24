using Game.Shared.HorizontalMover;
using Tools;
using Tools.Enums;
using UnityEngine;

namespace Game.Enemy
{
    public class Slime : MonoBehaviour
    {
        [SerializeField] private HeroHurtboxDetector _hurtboxDetector = null!;
        [SerializeField] private HorizontalPeriodicMover _mover = null!;
        [SerializeField] private float _speed;
        
        private void Awake()
        {
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
    }
}
