using System;
using UnityEngine;

namespace Game.Room
{
    [RequireComponent(typeof(Collider2D))]
    public class RoomHeroEnteringNotifier : MonoBehaviour
    {
        public event Action<Hero.Hero>? OnHeroEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var hero = other.GetComponentInParent<Hero.Hero>();
            if (!hero)
                return;
            
            OnHeroEntered?.Invoke(hero);
        }
    }
}
