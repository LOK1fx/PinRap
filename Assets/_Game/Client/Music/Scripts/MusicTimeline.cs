using UnityEngine;
using System;

namespace LOK1game
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicTimeline : Singleton<MusicTimeline>
    {
        private const float MAX_AUDIO_SOURCE_VOLUME = 1f;

        public event Action<MusicNode> OnBeat;
        public event Action<MusicNode> OnEndBeat;

        public bool IsPlaying { get; private set; }
        public bool PlaybackResumed { get; private set; }

        private MusicData _music;
        private float _currentSecond = 0;
        private int _position;

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
                TryBeatNextNode(OnBeatNode);
            else
                StopPlayback();

            _currentSecond += Time.deltaTime;
        }

        private void OnGameStateChanged(Game.EGameState newGameState)
        {
            switch (newGameState)
            {
                case Game.EGameState.Gameplay:
                    ResumePlayback();
                    break;
                case Game.EGameState.Paused:
                    OnGameStatePaused();
                    break;
            }
        }

        private void OnBeatNode(MusicNode node)
        {
            OnBeat?.Invoke(node);

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    ClientApp.ClientContext.BeatController.InstantiateBeat(node.Data.Strength);

            //    node.Play();

            //    _position++;
            //}
        }

        public void StartPlayback(MusicData music, float second = 0)
        {
            IsPlaying = true;
            PlaybackResumed = false;

            var musicInstance = ScriptableObject.CreateInstance<MusicData>();

            musicInstance = music;

            _music = musicInstance;
            _source.clip = _music.MusicClip;
            _source.Play();
            _currentSecond = second;
            _source.time = second;
        }

        public void OnGameStatePaused()
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
        }

        public bool TryBeatNextNode(Action<MusicNode> callback)
        {
            if (IsNodeInRange(_music.Nodes[_position]))
            {
                callback?.Invoke(_music.Nodes[_position]);

                return true;
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
    }
}