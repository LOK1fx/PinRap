using System.Collections;
using UnityEngine;

namespace LOK1game.Game
{
    public sealed class DefaultGameMode : BaseGameMode
    {
        [Space]
        [SerializeField] private PinRapEnemy _defaultEnemyPrefab;
        [SerializeField] private BeatEffectController _beatController;
        [SerializeField] private PauseController _pauseController;

        private EnemyManager _enemyManager;

        public override EGameModeId Id => EGameModeId.Default;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            _enemyManager = new EnemyManager();
            
            SpawnGameModeObject(CameraPrefab);
            SpawnGameModeObject(UiPrefab);
            SpawnGameModeObject(_pauseController.gameObject);

            //So strange code
            var player = SpawnGameModeObject(PlayerPrefab.gameObject);
            player.transform.position = GetRandomSpawnPointPosition();

            CreatePlayerController(player.GetComponent<Pawn>());

            if(BeatEffectController.Instance == null)
                Instantiate(_beatController);
            
            _enemyManager.CreateEnemy(_defaultEnemyPrefab);

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            State = EGameModeState.Ending;

            yield return DestroyAllGameModeObjects();
            yield return _enemyManager.DestroyAllEnemies();

            State = EGameModeState.Ended;
        }
    }
}