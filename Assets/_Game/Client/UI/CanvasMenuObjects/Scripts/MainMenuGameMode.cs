using System.Collections;
using LOK1game.Game;

namespace CanvasScripts
{
    public class MainMenuGameMode : BaseGameMode
    {
        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;
            
            StartCoroutine(DestroyAllGameModeObjects());
            
            State = EGameModeState.Started;
            
            yield return null;
        }
    
        public override IEnumerator OnEnd()
        {
            
            yield return null;
        }
    }
}
