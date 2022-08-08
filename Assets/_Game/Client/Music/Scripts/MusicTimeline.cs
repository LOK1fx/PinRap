using UnityEngine;
using System;
using LOK1game.Game;

namespace LOK1game
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicTimeline : Singleton<MusicTimeline>
    {
        #region Events

        public event Action OnMusicStart;
        public event Action OnMusicEnd;
        public event BeatDelegate OnBeat;
        public event BeatDelegate OnEndBeat;

        public delegate void BeatDelegate(MusicNode node);

        #endregion

        public bool IsPlaying { get; private set; }
        public bool PlaybackResumed { get; private set; }
        public MusicData MusicDataInstance { get; private set; }

        private MusicData _music;
        private float _currentSecond = 0;
        private int _position;
        private bool _isOnBeat;

        private const float MAX_AUDIO_SOURCE_VOLUME = 1f;

        private AudioSource _source;

        protected override void Awake()
        {
            base.Awake();

            _source = GetComponent<AudioSource>();

            App.ProjectContext.GameStateManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            if (!IsPlaying || _music == null) { return; }

            if (_position < _music.Nodes.Count)
                TryBeatNextNode(OnBeatNode, OutBeatNode);
            else
                StopPlayback();

            _currentSecond += Time.deltaTime;
        }

        private void OnGameStateChanged(EGameState newGameState)
        {
            switch (newGameState)
            {
                case EGameState.Gameplay:
                    ResumePlayback();
                    break;
                case EGameState.Paused:
                    OnGameStatePaused();
                    break;
            }
        }

        private void OnBeatNode(MusicNode node)
        {
            _isOnBeat = true;

            OnBeat?.Invoke(node);
        }

        private void OutBeatNode(MusicNode node)
        {
            _isOnBeat = false;

            _position++;

            OnEndBeat?.Invoke(node);
        }

        public bool TryBeat(out MusicNode beatedNode)
        {
            if (_isOnBeat == false)
            {
                beatedNode = null;

                return false;
            }

            var node = _music.Nodes[_position];

            node.Play();
            beatedNode = node;

            _position++;

            return true;
        }

        public void StartPlayback(MusicData music, float second = 0)
        {
            IsPlaying = true;
            PlaybackResumed = false;

            MusicDataInstance = ScriptableObject.CreateInstance<MusicData>();

            MusicDataInstance = music;

            _music = MusicDataInstance;
            _source.clip = _music.MusicClip;
            _source.Play();
            _currentSecond = second;
            _source.time = second;

            OnMusicStart?.Invoke();
        }

        private void OnGameStatePaused()
        {
            IsPlaying = false;
            PlaybackResumed = false;

            _source.volume = _music != null ? _music.MusicVolumeOutOfFocus : 0.25f;
        }

        public void ResumePlayback()
        {
            IsPlaying = true;
            PlaybackResumed = true;

            _source.Play();
            _source.volume = MAX_AUDIO_SOURCE_VOLUME;
            _source.time = _currentSecond;
        }

        public void EndPlayback()
        {
            IsPlaying = false;
            PlaybackResumed = false;

            foreach (var node in _music.Nodes)
            {
                node.Restore();
            }

            _source.Stop();
            _source.clip = null;
            _source.time = 0;
            _currentSecond = 0;
            _position = 0;
        }

        public void StopPlayback()
        {
            IsPlaying = false;

            OnMusicEnd?.Invoke();
        }

        public bool TryBeatNextNode(BeatDelegate inRangeCallback, BeatDelegate outRangeCallback)
        {
            if (IsNodeInRange(GetCurrentNode()))
            {
                inRangeCallback?.Invoke(GetCurrentNode());

                return true;
            }
            else if(IsNodeOutRange(GetCurrentNode()))
            {
                outRangeCallback?.Invoke(GetCurrentNode());

                return false;
            }

            return false;
        }

        private bool IsNodeInRange(MusicNode node)
        {
            if (node.StartSecond >= _currentSecond && node.StartSecond <= _currentSecond + _music.SecondError && !node.IsPlayed())
                return true;

            return false;
        }

        private bool IsNodeOutRange(MusicNode node)
        {
            if (_currentSecond > node.StartSecond + _music.SecondError && !node.IsPlayed())
                return true;

            return false;
        }

        private MusicNode GetCurrentNode()
        {
            if (_music == null)
                return null;

            return _music.Nodes[_position];
        }

        public int GetCurrentPosition()
        {
            return _position;
        }

        public float GetCurrentSecond()
        {
            return _currentSecond;
        }
    }
}