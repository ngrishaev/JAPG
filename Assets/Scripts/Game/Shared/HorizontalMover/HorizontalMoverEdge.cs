using System;
using UnityEngine;

namespace Game.Shared.HorizontalMover
{
    [RequireComponent(typeof(Collider2D))]
    public class HorizontalMoverEdge : MonoBehaviour
    {
        public event Action? OnEdgeReached;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var horizontalMover = other.GetComponentInParent<HorizontalPeriodicMover>();
            if (!horizontalMover)
                return;
            
            OnEdgeReached?.Invoke();
        }
    }
}