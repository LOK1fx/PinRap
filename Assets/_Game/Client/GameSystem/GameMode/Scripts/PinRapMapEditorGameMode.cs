using System.Collections;

namespace LOK1game.Game
{
    public class PinRapMapEditorGameMode : BaseGameMode
    {
        public override EGameModeId Id => EGameModeId.PinRapMapEditor;
        
        public override IEnumerator OnStart()
        {
            var player = SpawnGameModeObject(PlayerPrefab.gameObject);
            
            RegisterGameModeObject(CreatePlayerController(player.GetComponent<Pawn>()).gameObject);

            SpawnGameModeObject(CameraPrefab);

            if (UiPrefab != null)
                SpawnGameModeObject(UiPrefab);
            
            yield return null;
        }
        
        public override IEnumerator OnEnd()
        {
            yield return DestroyAllGameModeObjects();
        }
    }
}