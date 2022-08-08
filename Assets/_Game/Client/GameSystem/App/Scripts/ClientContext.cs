using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LOK1game
{
    [Serializable]
    public class ClientContext : Context
    {
        public event Action OnInitialized;

        private const string MUSIC_DATABASE_PATH = "MainMusicDatabase";
        
        public BeatController BeatController { get; private set; }
        public MusicDatabase MusicDatabase { get; private set; }

        [SerializeField] private TransitionLoad _loadingScreenPrefab;
        private TransitionLoad _loadingScreen;

        public override void Initialize()
        {
            MusicDatabase = Resources.Load<MusicDatabase>(MUSIC_DATABASE_PATH);
            BeatController = new BeatController();

            _loadingScreen = Object.Instantiate(_loadingScreenPrefab);
            Object.DontDestroyOnLoad(_loadingScreen.gameObject);

            OnInitialized?.Invoke();
        }
    }
}