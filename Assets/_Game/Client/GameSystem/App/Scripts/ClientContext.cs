using System;
using UnityEngine;

namespace LOK1game
{
    [Serializable]
    public class ClientContext : Context
    {
        public event Action OnInitialized;

        private const string MUSIC_DATABASE_PATH = "MainMusicDatabase";
        
        public BeatController BeatController { get; private set; }
        public MusicDatabase MusicDatabase { get; private set; }
        

        public override void Initialize()
        {
            MusicDatabase = Resources.Load<MusicDatabase>(MUSIC_DATABASE_PATH);
            BeatController = new BeatController();

            OnInitialized?.Invoke();
        }
    }
}