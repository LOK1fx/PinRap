using UnityEngine;

namespace LOK1game
{
    /// <summary>
    /// TODO: Split bpm counter into different script
    /// </summary>
    public class PermomentBeat : MonoBehaviour
    {
        public int BPM => _beatsPerMinute;
        
        public int BeatCountFull { get; private set; }

        [SerializeField] private EBeatEffectStrength _beatEffectStrength = EBeatEffectStrength.Weak;
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
                
                ClientApp.ClientContext.BeatController.InstantiateBeat(_beatEffectStrength);
            }
        }
    }
}