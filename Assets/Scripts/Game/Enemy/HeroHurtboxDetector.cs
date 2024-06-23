using System;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class HeroHurtboxDetector : MonoBehaviour
    {
        public event Action<Hero.Hero>? OnHeroTriggered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var hero = other.GetComponentInParent<Hero.Hero>();
            if (!hero)
                return;
            
            OnHeroTriggered?.Invoke(hero);
        }
    }
}
