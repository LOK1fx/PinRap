using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    public class GameEditorGizmo : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private Camera _camera;
        private GameEditorObject _target;

        public void Setup(GameEditorObject targetObject, Camera camera)
        {
            _camera = camera;
            _target = targetObject;
            OnTargetUpdated();

            _target.Updated += OnTargetUpdated;
        }

        private void OnTargetUpdated()
        {
            _image.sprite = _target.GetSprite();
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            var position = _camera.WorldToScreenPoint(_target.transform.position);
            position.z = 10f;

            transform.position = position;
        }

        private void OnDestroy()
        {
            _target.Updated -= OnTargetUpdated;
        }
    }
}