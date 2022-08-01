using System.Collections;
using System.Collections.Generic;
using LOK1game.Game;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.PinRap.Game
{
    public class PinRapTutorialGameMode : BaseGameMode
    {
        [Space]
        [SerializeField] private BeatEffectController _beatController;
        [SerializeField] private PauseController _pauseController;

        public override EGameModeId Id => EGameModeId.PinRapTutorial;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            SpawnGameModeObject(CameraPrefab);
            SpawnGameModeObject(UiPrefab);
            SpawnGameModeObject(_pauseController.gameObject);

            //So strange code
            var player = SpawnGameModeObject(PlayerPrefab.gameObject);
            player.transform.position = GetRandomSpawnPointPosition();

            CreatePlayerController(player.GetComponent<Pawn>());

            if(BeatEffectController.Instance == null)
                Instantiate(_beatController);
            
            EnemyManager.CreateEnemy(WorldEnemy.Instance.EnemyPrefab);
            
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