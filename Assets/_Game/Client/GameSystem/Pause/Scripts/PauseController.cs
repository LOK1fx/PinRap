using UnityEngine;
using LOK1game.Game;

namespace LOK1game
{
    public class PauseController : PersistentSingleton<PauseController>
    {
        [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
        
        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
                SwitchPauseState();
        }

        private void SwitchPauseState()
        {
            var manager = App.ProjectContext.GameStateManager;

            if (manager.CurrentGameState == EGameState.Gameplay)
            {
                manager.SetState(EGameState.Paused);
            }
            else
            {
                manager.SetState(EGameState.Gameplay);
            }
        }
    }
}