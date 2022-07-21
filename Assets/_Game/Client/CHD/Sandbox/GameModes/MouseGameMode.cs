using System.Collections;
using LOK1game.Game;

namespace LOK1game.Sandbox
{
    public class MouseGameMode : BaseGameMode
    {
        public override EGameModeId Id => EGameModeId.None; //If that id missed, just replace to None or delete this game mode

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            var camera = Instantiate(CameraPrefab);

            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>();
            var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            var player = spawnPoint.SpawnActor(PlayerPrefab);

            RegisterGameModeObject(camera);
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