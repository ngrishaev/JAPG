using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstraper _bootstrap = null!;
        
        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstraper>();

            if (bootstraper)
                return;

            Instantiate(_bootstrap);
        }
    }
}