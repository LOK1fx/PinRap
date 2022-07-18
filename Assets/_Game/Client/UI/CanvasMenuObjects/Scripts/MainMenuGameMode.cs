using System.Collections;
using LOK1game.Game;

namespace _Game.Client.UI.CanvasMenuObjects.Scripts
{
    public class MainMenuGameMode : BaseGameMode
    {
        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;
            
            DestroyAllGameModeObjects();
            
            State = EGameModeState.Started;
            
            yield return null;
        }
    
        public override IEnumerator OnEnd()
        {
            
            yield return null;
        }
    }
}
