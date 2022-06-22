using System.Collections;

namespace LOK1game.Game
{
    public sealed class DefaultGameMode : BaseGameMode
    {
        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            var camera = Instantiate(CameraPrefab);
            var ui = Instantiate(UiPrefab);

            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>();
            var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length + 1)];
            var player = spawnPoint.SpawnActor(PlayerPrefab);

            RegisterGameModeObject(camera);
            RegisterGameModeObject(ui);
            RegisterGameModeObject(player.gameObject);

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