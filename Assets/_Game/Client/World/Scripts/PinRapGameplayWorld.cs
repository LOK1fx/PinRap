using LOK1game.World;
using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

namespace LOK1game.PinRap.World
{
    [RequireComponent(typeof(WorldEnemy))]
    public class PinRapGameplayWorld : GameWorld
    {
        public MusicData MusicData => _musicData;
        public LevelConfigData LevelConfigData => _levelConfigData;
        
        [SerializeField] private MusicData _musicData;
        [SerializeField] private LevelConfigData _levelConfigData;

        protected override void Initialize()
        {
#if UNITY_EDITOR

            if (SceneManager.GetSceneByName("PinRapGameplayCore").isLoaded == false)
                SceneManager.LoadScene("PinRapGameplayCore", LoadSceneMode.Additive);

#endif
        }
    }
}