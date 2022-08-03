using System.Collections.Generic;
using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game.World
{
    public abstract class GameWorld : MonoBehaviour
    {
        public static GameWorld Current { get; private set; }
        public EGameModeId StandardGameModeOverride => _standardGameModeOverride;
        
        [SerializeField] private EGameModeId _standardGameModeOverride;

        protected void Awake()
        {
            Current = this;

            var gameModeManager = ProjectContext.GetGameModeManager();

            if (_standardGameModeOverride != EGameModeId.None)
            {
                gameModeManager.SetGameMode(_standardGameModeOverride);
            }
            else
            {
                gameModeManager.SetGameMode(App.ProjectContext.StandardGameModeId);
            }
            
            Initialize();
        }

        private void OnDestroy()
        {
            Current = null;
        }

        public static bool TryGetWorld<T>(out T world) where T : GameWorld
        {
            if (Current != null && Current is T)
            {
                world = Current as T;
                return true;
            }

            world = null;
            return false;
        }

        protected abstract void Initialize();
    }
}