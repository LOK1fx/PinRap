using UnityEngine;

namespace LOK1game
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad;
        
        public void ChangeLevel()
        {
            try
            {
                TransitionLoad.LoadLevel(_levelToLoad);
            }
            catch
            {
                App.Quit();
            }
        }
    }
}