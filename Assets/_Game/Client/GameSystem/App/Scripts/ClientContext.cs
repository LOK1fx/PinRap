using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LOK1game
{
    [Serializable]
    public class ClientContext : Context
    {
        public event Action OnInitialized;
    
        public BeatController BeatController { get; private set; }
        public PostProcessingController PostProcessingController { get; private set; }

        [SerializeField] private TransitionLoad _loadingScreenPrefab;
        private TransitionLoad _loadingScreen;

        public override void Initialize()
        {
            BeatController = new BeatController();
            
            PostProcessingController = new PostProcessingController();
            PostProcessingController.UpdateState();

            _loadingScreen = Object.Instantiate(_loadingScreenPrefab);
            Object.DontDestroyOnLoad(_loadingScreen.gameObject);

            OnInitialized?.Invoke();
        }
    }
}