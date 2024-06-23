using System;
using UnityEngine;

namespace Game.Enemy
{
    public class Slime : MonoBehaviour
    {
        [SerializeField] private HeroHurtboxDetector _hurtboxDetector = null!;

        private void Awake()
        {
            _hurtboxDetector.OnHeroTriggered += OnHeroHit;
        }

        private void OnHeroHit(Hero.Hero hero)
        {
            hero.Damage();
        }
    }
}
