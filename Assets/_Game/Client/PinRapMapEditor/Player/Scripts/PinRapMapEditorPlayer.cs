using Cinemachine;
using UnityEngine;

namespace LOK1game.PinRap
{
    public class PinRapMapEditorPlayer : Pawn
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private float _zoomChangeSpeed = 1f;
        [SerializeField, Range(ZOOM_MIN_RANGE, 10f)] private float _zoomMaxRange;

        private const float ZOOM_MIN_RANGE = 2f;

        private float _currentZoom;
        private Vector3 _startDragPosition;
        private bool _isDragging;

        private void Update()
        {
            if (_isDragging)
            {
                _cameraTransform.localPosition += (_startDragPosition - Input.mousePosition) * (_moveSpeed * Time.deltaTime);
            }

            _camera.m_Lens.OrthographicSize = _currentZoom;
        }

        public override void OnPocces(Controller sender)
        {
            _camera.Priority = 100;
        }

        public override void OnInput(object sender)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                _startDragPosition = Input.mousePosition;
                
                _isDragging = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                _isDragging = false;
            }

            _currentZoom -= Input.mouseScrollDelta.y * _zoomChangeSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, ZOOM_MIN_RANGE, _zoomMaxRange);
        }
    }

}