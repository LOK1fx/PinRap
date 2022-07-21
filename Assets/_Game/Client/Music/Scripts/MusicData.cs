using System;
using System.Collections.Generic;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class MusicData : ScriptableObject
    {
        public float MusicVolumeOutOfFocus => _musicVolumeOutOfFocus;
        public List<MusicNode> Nodes => _nodes;
        public AudioClip MusicClip => _music;
        public float SecondError => _secondError;

        [SerializeField, Range(0f, 1f)] private float _musicVolumeOutOfFocus = 0.25f;
        [SerializeField] private List<MusicNode> _nodes = new List<MusicNode>();
        [SerializeField] private AudioClip _music;
        [SerializeField] private float _secondError = 0.15f;
    }

    [Serializable]
    public class MusicNode
    {
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
