using System;
using UnityEngine;

namespace LOK1game
{
    public class BeatsPerMinuteCounter : MonoBehaviour
    {
        public event Action OnBeat;
        
        public int CurrentBPM => _beatsPerMinute;
        
        [SerializeField, Range(2, 240)] private int _beatsPerMinute = 60;

        private float _beatInterval;
        private float _beatTimer;
        
        private void Update()
        {
            BeatDetection();
        }

        private void BeatDetection()
        {
            _beatInterval = Constants.General.TIME_MINUTE / _beatsPerMinute;
            _beatTimer += Time.deltaTime;

            if (_beatTimer >= _beatInterval)
            {
                _beatTimer -= _beatInterval;
                
                OnBeat?.Invoke();
            }
        }
    }
}