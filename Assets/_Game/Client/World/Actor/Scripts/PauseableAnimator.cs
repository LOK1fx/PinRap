using UnityEngine;
using LOK1game.Game;

namespace LOK1game
{
    [RequireComponent(typeof(Animator))]
    public class PauseableAnimator : MonoBehaviour
    {
        private Animator _animator;
        private float _animatorSpeedBeforePaused = 1f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            App.ProjectContext.GameStateManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(EGameState newGameState)
        {
            if(newGameState == EGameState.Paused)
            {
                _animatorSpeedBeforePaused = _animator.speed;
                _animator.speed = 0;
            }
            else
            {
                _animator.speed = _animatorSpeedBeforePaused;
            }
        }

        private void OnDestroy()
        {
            App.ProjectContext.GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}