using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress.SaveLoad;
using Infrastructure.Services.Reset;
using UnityEngine;

namespace Game.Level
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider = null!;
        
        private ISaveLoadService _saveLoadService = null!;
        private IResetService _resetService = null!;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _resetService = AllServices.Container.Single<IResetService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _saveLoadService.SaveProgress();
            _resetService.Reset();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + (Vector3)_collider.offset, _collider.size);
        }
    }
}