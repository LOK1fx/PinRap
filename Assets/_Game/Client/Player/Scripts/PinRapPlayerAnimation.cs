using UnityEngine;

namespace LOK1game.PinRap
{
    [RequireComponent(typeof(PinRapPlayer), typeof(Animator))]
    public class PinRapPlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private PinRapPlayer _player;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _player = GetComponent<PinRapPlayer>();
            _player.OnDie += OnDie;

            _player.Input.OnLeftArrowPressed += OnLeftArrowPressed;
            _player.Input.OnUpArrowPressed += OnUpArrowPressed;
            _player.Input.OnDownArrowPressed += OnDownArrowPressed;
            _player.Input.OnRightArrowPressed += OnRightArrowPressed;
        }

        private void OnDie()
        {
            PlayAction(PinRapCharacterAnimationConstants.ACTION_DIE);
        }

        private void OnDestroy()
        {
            _player.OnDie -= OnDie;
            
            _player.Input.OnLeftArrowPressed -= OnLeftArrowPressed;
            _player.Input.OnUpArrowPressed -= OnUpArrowPressed;
            _player.Input.OnDownArrowPressed -= OnDownArrowPressed;
            _player.Input.OnRightArrowPressed -= OnRightArrowPressed;
        }

        private void OnLeftArrowPressed()
        {
            PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_LEFT);
        }
        
        private void OnUpArrowPressed()
        {
            PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_UP);
        }
        
        private void OnDownArrowPressed()
        {
            PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_DOWN);
        }
        private void OnRightArrowPressed()
        {
            PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_RIGHT);
        }

        private void PlayAction(string actionName)
        {
            _animator.Play(actionName, 0, 0f);
        }
    }
}