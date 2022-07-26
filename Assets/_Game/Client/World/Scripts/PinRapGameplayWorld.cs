using UnityEngine.SceneManagement;

namespace LOK1game.World
{
    public class PinRapGameplayWorld : GameWorld
    {
        protected override void Initialize()
        {
#if UNITY_EDITOR
            if (StandardGameModeOverride == EGameModeId.Default)
                SceneManager.LoadSceneAsync("PinRapGameplayCore", LoadSceneMode.Additive);
#endif
        }
    }
}