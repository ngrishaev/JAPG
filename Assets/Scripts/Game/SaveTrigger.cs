using System;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress.SaveLoad;
using UnityEngine;

namespace Game
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider = null!;
        
        private ISaveLoadService _saveLoadService = null!;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + (Vector3)_collider.offset, _collider.size);
        }
    }
}