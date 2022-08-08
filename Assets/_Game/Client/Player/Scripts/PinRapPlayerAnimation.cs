using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(PinRapPlayer), typeof(Animator))]
    public class PinRapPlayerAnimation : MonoBehaviour
    {
        private const string ACTION_BEAT_LEFT = "Character_BeatLeft";
        private const string ACTION_BEAT_UP = "Character_BeatUp";
        private const string ACTION_BEAT_DOWN = "Character_BeatDown";
        private const string ACTION_BEAT_RIGHT = "Character_BeatRight";
        private const string ACTION_DIE = "Character_Die";
        
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
            PlayAction(ACTION_DIE);
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
            PlayAction(ACTION_BEAT_LEFT);
        }
        
        private void OnUpArrowPressed()
        {
            PlayAction(ACTION_BEAT_UP);
        }
        
        private void OnDownArrowPressed()
        {
            PlayAction(ACTION_BEAT_DOWN);
        }
        private void OnRightArrowPressed()
        {
            PlayAction(ACTION_BEAT_RIGHT);
        }

        private void PlayAction(string actionName)
        {
            _animator.Play(actionName, 0, 0f);
        }
    }
}