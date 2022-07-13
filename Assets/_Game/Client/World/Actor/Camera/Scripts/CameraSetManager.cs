using UnityEngine;
using Cinemachine;

namespace LOK1game
{
    public class CameraSetManager : MonoBehaviour
    {
        private const int MAX_PRIORITY = 100;
        private const int MIN_PRIORITY = 0;

        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _enemyCamera;
        [SerializeField] private CinemachineVirtualCamera _centerCamera;

        public void SetFocusOnPlayer()
        {
            SetFocus(_playerCamera);
        }

        public void SetFocusOnEnemy()
        {
            SetFocus(_enemyCamera);
        }

        public void SetFocusOnCenter()
        {
            SetFocus(_centerCamera);
        }

        public void SetFocusOnMain()
        {
            SetFocus(_mainCamera);
        }

        private void SetFocus(CinemachineVirtualCamera camera)
        {
            SetPriorityOnAllCameras(MIN_PRIORITY);

            camera.Priority = MAX_PRIORITY;
        }

        private void SetPriorityOnAllCameras(int priority)
        {
            _mainCamera.Priority = priority;
            _playerCamera.Priority = priority;
            _enemyCamera.Priority = priority;
            _centerCamera.Priority = priority;
        }
    }
}