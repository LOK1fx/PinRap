using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game.PinRap.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu Instance { get; private set; }
        
        [SerializeField] private LevelData _mainLevelData;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadToLevel(LevelData data)
        {
            TransitionLoad.LoadLevel(data);
        }

        public void LoadToGame()
        {
            TransitionLoad.LoadLevel(_mainLevelData);
        }
    }
}