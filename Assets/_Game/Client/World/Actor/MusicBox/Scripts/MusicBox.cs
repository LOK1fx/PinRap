using UnityEngine;

namespace LOK1game.World
{
    public class MusicBox : MonoBehaviour, IBeatReaction
    {
        [SerializeField] private float _sizeChangeSpeed = 8f;
        [SerializeField] private float _sizeChangingMultiplier = 0.5f;

        private Vector3 _defaultSize;
        private bool _paused;

        private void Start()
        {
            ClientApp.ClientContext.BeatController.RegisterActor(this);
            App.ProjectContext.GameStateManager.OnGameStateChanged += OnGameStateChanged;

            _defaultSize = transform.localScale;
        }

        private void LateUpdate()
        {
            if(_paused) { return; }

            transform.localScale = Vector3.Lerp(transform.localScale, _defaultSize, Time.deltaTime * _sizeChangeSpeed);
        }

        public void OnBeat(EBeatEffectStrength strength)
        {
            transform.localScale += (Vector3.one * (1f / (int)strength) * _sizeChangingMultiplier);

            Debug.Log("Beat");
        }

        private void OnGameStateChanged(Game.EGameState newGameState)
        {
            if (newGameState == Game.EGameState.Paused)
            {
                _paused = true;
            }
            else
            {
                _paused = false;
            }
        }

        private void OnDestroy()
        {
            ClientApp.ClientContext.BeatController.UnregisterActor(this);
            App.ProjectContext.GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}