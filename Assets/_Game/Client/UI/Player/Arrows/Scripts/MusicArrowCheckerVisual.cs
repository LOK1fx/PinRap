using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    [RequireComponent(typeof(Image))]
    public class MusicArrowCheckerVisual : MonoBehaviour
    {
        [SerializeField] private Color _beatedColor = Color.white;
        [SerializeField] private float _speed = 8f;
        
        private Image _image;
        private Color _targetColor;
        private Color _defaultColor;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
        }

        private void Update()
        {
            _image.color = Color.Lerp(_image.color, _targetColor, Time.deltaTime * _speed);
            _targetColor = Color.Lerp(_targetColor, _defaultColor, Time.deltaTime * _speed);
        }

        public void Beat()
        {
            _targetColor = _beatedColor;
        }
    }
}