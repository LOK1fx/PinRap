using System.Collections;
using LOK1game.PinRap;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game.Game
{
    public sealed class DefaultGameMode : BaseGameMode
    {
        [Space]
        [SerializeField] private BeatEffectController _beatController;
        [SerializeField] private PauseController _pauseController;

        private IPlayerHud _playerHud;

        public override EGameModeId Id => EGameModeId.Default;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            SpawnGameModeObject(CameraPrefab);
            SpawnGameModeObject(_pauseController.gameObject);

            var player = SpawnGameModeObject(PlayerPrefab.gameObject);
            player.transform.position = GetRandomSpawnPointPosition();

            var playerController = CreatePlayerController(player.GetComponent<Pawn>());
            var uiGameObject = SpawnGameModeObject(UiPrefab);

            _playerHud = uiGameObject.GetComponent<IPlayerHud>();
            _playerHud.Bind(playerController, player.GetComponent<PinRapPlayer>());

            if (BeatEffectController.Instance == null)
                Instantiate(_beatController);
            
            EnemyManager.CreateEnemy(WorldEnemy.Instance.EnemyPrefab);
            
            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            State = EGameModeState.Ending;

            _playerHud.Unbind();

            yield return DestroyAllGameModeObjects();

            State = EGameModeState.Ended;
        }
    }
}