using UnityEngine;
using Cinemachine;

namespace LOK1game.Player
{
    public class PlayerCamera : Actor, IPawn
    {
        public float Tilt;

        [SerializeField] private float _sensivity = 8f;
        [SerializeField] private Transform _cameraTransform;

        [Space]
        [SerializeField] private float _maxRightViewAngle = 30f;
        [SerializeField] private float _maxLeftViewAngle = 30f;

        [Space]
        [SerializeField] private float _maxUpViewAngle = 15f;
        [SerializeField] private float _maxDownViewAngle = 15f;

        [Space]
        [SerializeField] private float _defaultFov = 65f;

        [HideInInspector] public Vector3 DesiredPosition;

        [SerializeField] private float _cameraOffsetResetSpeed = 7f;

        private Vector3 _cameraLerpOffset;

        [SerializeField] private Transform _recoilCamera;
        [SerializeField] private float _recoilCameraRotationSpeed;
        [SerializeField] private float _recoilCameraReturnSpeed;

        private Vector3 _recoilCameraRotation;
        private Vector3 _currentRecoilCameraRotation;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            DesiredPosition = _cameraTransform.localPosition;

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        private void LateUpdate()
        {
            var targetRot = Quaternion.Euler(_yRotation, _xRotation, Tilt);

            _cameraTransform.localRotation = targetRot;

            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, DesiredPosition + _cameraLerpOffset, Time.deltaTime * _cameraOffsetResetSpeed);
            _cameraLerpOffset = Vector3.Lerp(_cameraLerpOffset, Vector3.zero, Time.deltaTime * _cameraOffsetResetSpeed);
        }

        private void FixedUpdate()
        {
            _recoilCameraRotation = Vector3.Lerp(_recoilCameraRotation, Vector3.zero, _recoilCameraReturnSpeed * Time.deltaTime);
            _currentRecoilCameraRotation = Vector3.Slerp(_currentRecoilCameraRotation, _recoilCameraRotation, _recoilCameraRotationSpeed * Time.fixedDeltaTime);
            _recoilCamera.localRotation = Quaternion.Euler(_currentRecoilCameraRotation);
        }

        public void TriggerRecoil(Vector3 recoil)
        {
            _recoilCameraRotation += new Vector3(-recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
        }

        public void OnInput(object sender)
        {
            var x = 0f;
            var y = 0f;

            if (!Application.isMobilePlatform)
            {
                x = Input.GetAxis("Mouse X");
                y = Input.GetAxis("Mouse Y");
            }
            else
            {
                if (Input.touchCount >= 1)
                {
                    x = Input.GetTouch(0).deltaPosition.x;
                    y = Input.GetTouch(0).deltaPosition.y;
                }
            }

            _xRotation += x * GetSensivityMultiplier();
            _yRotation -= y * GetSensivityMultiplier();

            _xRotation = Mathf.Clamp(_xRotation, -_maxLeftViewAngle, _maxRightViewAngle);
            _yRotation = Mathf.Clamp(_yRotation, -_maxUpViewAngle, _maxDownViewAngle);

            _xRotation = ThreehoundredToZero(_xRotation);
            _yRotation = ThreehoundredToZero(_yRotation);
        }

        private float GetSensivityMultiplier()
        {
            var multiplier = 20f;

            if(Application.isMobilePlatform)
            {
                multiplier = 1f;
            }

            return (_sensivity * multiplier) * Time.deltaTime;
        }

        private float ThreehoundredToZero(float value)
        {
            if(value >= 360 || value <= -360)
            {
                return 0f;
            }
            else
            {
                return value;
            }
        }

        public void AddCameraOffset(Vector3 offset)
        {
            _cameraLerpOffset += offset;
        }

        public Transform GetCameraTransform()
        {
            return _cameraTransform;
        }

        public Transform GetRecoilCameraTransform()
        {
            return _recoilCamera.transform;
        }

        public float GetDefaultFov()
        {
            return _defaultFov;
        }

        public void OnPocces(Controller sender)
        {
            throw new System.NotImplementedException();
        }
    }
}