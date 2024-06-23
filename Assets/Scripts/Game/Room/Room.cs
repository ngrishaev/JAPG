using System.Collections;
using UnityEngine;

namespace Game.Room
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomHeroEnteringNotifier _heroEnteringNotifier = null!;
        private Vector3 _roomPosition;
        private Camera _mainCamera = null!;

        private void Awake()
        {
            _heroEnteringNotifier.OnHeroEntered += OnHeroEntered;
            _roomPosition = transform.position;
        }

        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void OnHeroEntered(Hero.Hero hero)
        {
            StartCoroutine(MoveCameraToRoomRoutine());
        }

        private IEnumerator MoveCameraToRoomRoutine()
        {
            var cameraPosition = _mainCamera.transform.position;
            
            var targetPosition = new Vector3(_roomPosition.x, _roomPosition.y + 0.5f, cameraPosition.z);
            var time = 0f;
            while (time < 1f)
            {
                time += Time.deltaTime;
                _mainCamera.transform.position = Vector3.Lerp(cameraPosition, targetPosition, time);
                yield return null;
            }
        }
    }
}
