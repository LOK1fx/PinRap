using UnityEngine;

namespace LOK1game
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad;
        
        public void ChangeLevel()
        {
            TransitionLoad.LoadLevel(_levelToLoad);
        }
    }
}