using System;
using System.Collections;
using Tools;
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
            if(GetCameraPosition().IsCloseEnough(_mainCamera.transform.position))
                return;
            
            FreezeWorld();
            StartCoroutine(MoveCameraToRoomRoutine(onFinish: UnfreezeWorld));
        }

        private void UnfreezeWorld()
        {
            Time.timeScale = 1f;
        }

        private void FreezeWorld()
        {
            Time.timeScale = 0f;
        }

        private IEnumerator MoveCameraToRoomRoutine(Action onFinish)
        {
            // TODO: replace with do tween
            var cameraPosition = _mainCamera.transform.position;

            var targetPosition = GetCameraPosition().WithZ(cameraPosition.z);
            var time = 0f;
            while (time < 1f)
            {
                time += Time.unscaledDeltaTime;
                _mainCamera.transform.position = Vector3.Lerp(cameraPosition, targetPosition, time);
                yield return null;
            }

            onFinish();
        }

        private Vector2 GetCameraPosition()
        {
            return new Vector2(_roomPosition.x, _roomPosition.y + 0.5f);
        }
    }
}
