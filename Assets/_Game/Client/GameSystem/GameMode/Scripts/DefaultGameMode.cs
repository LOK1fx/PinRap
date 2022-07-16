using System.Collections;
using UnityEngine;

namespace LOK1game.Game
{
    public sealed class DefaultGameMode : BaseGameMode
    {
        [SerializeField] private BeatEffectController _beatController;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            var playerController = Instantiate(PlayerController);

            playerController.name = $"[{nameof(PlayerController)}]";

            SpawnGameModeObject(CameraPrefab);
            SpawnGameModeObject(UiPrefab);

            //So strange code
            var player = SpawnGameModeObject(PlayerPrefab.gameObject);
            
            player.transform.position = GetRandomSpawnPoint().transform.position;
            playerController.SetControlledPawn(player.GetComponent<Pawn>());

            if(BeatEffectController.Instance == null)
                Instantiate(_beatController);

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            State = EGameModeState.Ending;

            yield return DestroyAllGameModeObjects();

            State = EGameModeState.Ended;
        }
    }
}