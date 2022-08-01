using UnityEngine;
using LOK1game.UI;

namespace LOK1game.PinRap
{
    [RequireComponent(typeof(AudioSource), typeof(PinRapCharacter))]
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _onLeftArrowBeated;
        [SerializeField] private AudioClip _onDownArrowBeated;
        [SerializeField] private AudioClip _onUpArrowBeated;
        [SerializeField] private AudioClip _onRightArrowBeated;
        
        private PinRapCharacter _character;
        private AudioSource _source;

        private void Awake()
        {
            _character = GetComponent<PinRapCharacter>();
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _character.OnBeatArrow += OnCharacterBeatArrow;
        }

        private void OnCharacterBeatArrow(EArrowType type)
        {
            switch (type)
            {
                case EArrowType.Left:
                    PlaySound(_onLeftArrowBeated);
                    break;
                case EArrowType.Down:
                    PlaySound(_onDownArrowBeated);
                    break;
                case EArrowType.Up:
                    PlaySound(_onUpArrowBeated);
                    break;
                case EArrowType.Right:
                    PlaySound(_onRightArrowBeated);
                    break;
            }
        }

        private void PlaySound(AudioClip clip)
        {
            _source.PlayOneShot(clip);
        }

        private void OnDestroy()
        {
            _character.OnBeatArrow -= OnCharacterBeatArrow;
        }
    }
}