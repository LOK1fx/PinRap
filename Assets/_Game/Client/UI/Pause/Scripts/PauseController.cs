using UnityEngine;
using LOK1game.Game;

namespace LOK1game
{
    public class PauseController : PersistentSingleton<PauseController>
    {
        public bool IsGamePaused { get; private set; }

        [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
                SwitchPauseState();
        }

        public static PauseController Spawn(PauseController prefab)
        {
            var controller = Instantiate(prefab);

            controller.name = prefab.name;

            DontDestroyOnLoad(controller);

            return controller;
        }

        private void SwitchPauseState()
        {
            var manager = App.ProjectContext.GameStateManager;

            if (manager.CurrentGameState == EGameState.Gameplay)
            {
                manager.SetState(EGameState.Paused);

                IsGamePaused = true;
            }
            else
            {
                manager.SetState(EGameState.Gameplay);

                IsGamePaused = false;
            }
        }
    }
}