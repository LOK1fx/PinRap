using LOK1game.Game;
using UnityEngine;

namespace LOK1game
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        
        private void Awake()
        {
            switch (App.ProjectContext.GameStateManager.CurrentGameState)
            {
                case EGameState.Gameplay:
                    _root.SetActive(false);
                    break;
                case EGameState.Paused:
                    _root.SetActive(true);
                    break;
            }
        }

        public void Resume()
        {
            App.ProjectContext.GameStateManager.SetState(EGameState.Gameplay);
        }
    }
}