using UnityEngine;

namespace LOK1game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupAlphaEvolutor : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField, Range(0, 1)] private float _targetAlpha = 1;
        [SerializeField, Range(0, 1)] private float _startAlpha = 1;

        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _canvas.alpha = _startAlpha;
        }

        private void Update()
        {
            _canvas.alpha = Mathf.Lerp(_canvas.alpha, _targetAlpha, Time.deltaTime * _speed);
        }

        public void SetTargetAlpha(float value)
        {
            value = Mathf.Clamp01(value);

            _targetAlpha = value;
        }
    }
}