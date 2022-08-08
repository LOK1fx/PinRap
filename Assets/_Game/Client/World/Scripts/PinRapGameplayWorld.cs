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
        protected override void Initialize()
        {
#if UNITY_EDITOR

            if (SceneManager.GetSceneByName("PinRapGameplayCore").isLoaded == false)
                SceneManager.LoadScene("PinRapGameplayCore", LoadSceneMode.Additive);

#endif
        }
    }
}