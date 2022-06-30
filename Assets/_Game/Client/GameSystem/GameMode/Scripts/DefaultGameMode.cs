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

            var camera = Instantiate(CameraPrefab);
            var ui = Instantiate(UiPrefab);

            if(BeatEffectController.Instance == null)
                Instantiate(_beatController);

            RegisterGameModeObject(camera);
            RegisterGameModeObject(ui);

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