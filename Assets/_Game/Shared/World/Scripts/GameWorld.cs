using System.Collections.Generic;
using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game.World
{
    public abstract class GameWorld : MonoBehaviour
    {
        public static GameWorld Current { get; private set; }
        

        [SerializeField] private EGameModeId _standardGameModeOverride;

        protected void Awake()
        {
            Current = this;
            
            EventManager.AddListener<OnProjectContextInitializedEvent>(OnProjectContextInitialized);
        }

        private void OnDestroy()
        {
            Current = null;
            
            EventManager.RemoveListener<OnProjectContextInitializedEvent>(OnProjectContextInitialized);
        }

        private void OnProjectContextInitialized(OnProjectContextInitializedEvent evt)
        {
            var gameModeManager = evt.ProjectContext.GameModeManager;
            
            Debug.Log("OnProjectContext initialized");

            if (gameModeManager.CurrentGameModeId == _standardGameModeOverride
                && gameModeManager.CurrentGameModeId != EGameModeId.None || _standardGameModeOverride == EGameModeId.None)
            {
                return;
            }

            gameModeManager.SetGameMode(_standardGameModeOverride);
        }

        public abstract void Initialize();
    }
}