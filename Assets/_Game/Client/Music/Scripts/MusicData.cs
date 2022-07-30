using System;
using System.Collections.Generic;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class MusicData : ScriptableObject
    {
        public AnimationCurve ArrowsSpeedGraph => _arrowsSpeedGraph;
        public float ArrowsBaseSpeed => _arrowsBaseSpeed;
        public float SecondError => _secondError;
        public int BPM => _bpm;
        public AudioClip MusicClip => _music;
        public float MusicVolumeOutOfFocus => _musicVolumeOutOfFocus;
        public List<MusicNode> Nodes => _nodes;


        [Header("Main")]
        [SerializeField] private AnimationCurve _arrowsSpeedGraph = AnimationCurve.Constant(0f, 1f, 1f);
        [SerializeField, Range(0f, 100f)] private float _arrowsBaseSpeed = 10f;
        [SerializeField] private float _secondError = 0.15f;

        [Header("Music")]
        [SerializeField] private int _bpm;
        [SerializeField] private AudioClip _music;
        [SerializeField, Range(0f, 1f)] private float _musicVolumeOutOfFocus = 0.25f;
        [SerializeField] private List<MusicNode> _nodes = new List<MusicNode>();
    }

    [Serializable]
    public class MusicNode
    {
        public bool Enemy;
        public float StartSecond;
        public EBeatEffectStrength BeatEffectStrength;
        public EArrowType ArrowType;

        private bool _isPlayed;

        public bool IsPlayed()
        {
            return _isPlayed;
        }

        public void Play()
        {
            _isPlayed = true;
        }

        public void Restore()
        {
            _isPlayed = false;
        }
    }
}
