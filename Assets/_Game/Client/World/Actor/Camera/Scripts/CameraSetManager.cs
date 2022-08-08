using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

namespace LOK1game
{
    public class CameraSetManager : MonoBehaviour
    {
        private const int MAX_PRIORITY = 100;
        private const int MIN_PRIORITY = 0;

        [SerializeField] private EStartCameraFocus _startFocus = EStartCameraFocus.Main;
        [SerializeField, Range(0f, 25f)] private float _startFocusSetDelay = 0f;
        
        [Space]
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _enemyCamera;
        [SerializeField] private CinemachineVirtualCamera _centerCamera;

        private void Awake()
        {
            if (_startFocus == 0)
                SetStartFocus(_startFocus);
            else
                StartCoroutine(SetStartFocusRoutine());
        }

        private IEnumerator SetStartFocusRoutine()
        {
            yield return new WaitForSeconds(_startFocusSetDelay);
            
            SetStartFocus(_startFocus);
        }

        private void SetStartFocus(EStartCameraFocus focus)
        {
            switch (focus)
            {
                case EStartCameraFocus.Center:
                    SetFocusOnCenter();
                    break;
                case EStartCameraFocus.Main:
                    SetFocusOnMain();
                    break;
                case EStartCameraFocus.Left:
                    SetFocusOnPlayer();
                    break;
                case EStartCameraFocus.Right:
                    SetFocusOnEnemy();
                    break;
                default:
                    SetFocusOnMain();
                    break;
            }
        }

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
        
        private enum EStartCameraFocus
        {
            Center,
            Main,
            Left,
            Right
        }
    }
}