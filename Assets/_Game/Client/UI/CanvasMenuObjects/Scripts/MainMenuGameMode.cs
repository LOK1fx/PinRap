using System.Collections;
using LOK1game.Game;

namespace LOK1game
{
    public class MainMenuGameMode : BaseGameMode
    {
        public override EGameModeId Id => EGameModeId.MainMenu;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            SpawnGameModeObject(UiPrefab);
            
            State = EGameModeState.Started;
            
            yield return null;
        }
    
        public override IEnumerator OnEnd()
        {
            yield return DestroyAllGameModeObjects();
        }
    }
}
