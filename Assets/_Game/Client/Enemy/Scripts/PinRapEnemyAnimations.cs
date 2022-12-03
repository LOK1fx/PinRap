using UnityEngine;

namespace LOK1game.PinRap
{
    [RequireComponent(typeof(PinRapEnemy), typeof(Animator))]
    public class PinRapEnemyAnimations : MonoBehaviour
    {
        private PinRapEnemy _character;
        private Animator _animator;

        private void Awake()
        {
            _character = GetComponent<PinRapEnemy>();
            _animator = GetComponent<Animator>();

            _character.OnSuccesfullBeat += OnBeatArrow;
        }

        private void OnDestroy()
        {
            _character.OnSuccesfullBeat -= OnBeatArrow;
        }

        private void OnBeatArrow(LOK1game.UI.EArrowType arrowType)
        {
            switch (arrowType)
            {
                case LOK1game.UI.EArrowType.None:
                    break;
                case LOK1game.UI.EArrowType.Left:
                    PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_RIGHT); //It's not a mistake that it's RIGHT
                    break;
                case LOK1game.UI.EArrowType.Down:
                    PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_DOWN);
                    break;
                case LOK1game.UI.EArrowType.Up:
                    PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_UP);
                    break;
                case LOK1game.UI.EArrowType.Right:
                    PlayAction(PinRapCharacterAnimationConstants.ACTION_BEAT_LEFT); //It's not a mistake that it's LEFT
                    break;
                default:
                    break;
            }
        }

        private void PlayAction(string actionName)
        {
            _animator.Play(actionName, 0, 0f);
        }
    }
}