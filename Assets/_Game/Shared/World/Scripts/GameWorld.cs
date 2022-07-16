using System.Collections.Generic;
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
            
            App.ProjectContext.OnInitialized += OnProjectContextInitialized;
        }

        private void OnDestroy()
        {
            Current = null;
        }

        private void OnProjectContextInitialized()
        {
            var gameModeManager = App.ProjectContext.GameModeManager;

            if (gameModeManager.CurrentGameModeId == _standardGameModeOverride
                && gameModeManager.CurrentGameModeId != EGameModeId.None || _standardGameModeOverride == EGameModeId.None)
            {
                return;
            }

            gameModeManager.SetGameMode(_standardGameModeOverride);
        }

        public abstract void Intialize();
    }
}