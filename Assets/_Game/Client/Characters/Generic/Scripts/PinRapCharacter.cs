using System;
using System.Collections;
using LOK1game.PinRap.World;
using LOK1game.UI;
using LOK1game.World;
using UnityEngine;

namespace LOK1game.PinRap
{
    public abstract class PinRapCharacter : Pawn, IDamagable
    {
        public event Action<EArrowType> OnBeatArrow;

        public CharacterData CharacterData => _characterData;
        public PinRapGameplayWorld World { get; private set; }

        [SerializeField] private CharacterData _characterData;

        [Header("Damage")]
        [SerializeField] private float _resetColorsDelay = 0.45f;
        [SerializeField] private Color _damagedColor = Color.red;
        
        private SpriteRenderer[] _allCharacterSprites;
        private Color[] _defaultSpritesColors;
        private Coroutine _currentResetColorRoutine;

        protected virtual void Awake()
        {
            World = GameWorld.GetWorld<PinRapGameplayWorld>();

            _allCharacterSprites = GetComponentsInChildren<SpriteRenderer>();
            _defaultSpritesColors = new Color[_allCharacterSprites.Length];

            for (int i = 0; i < _allCharacterSprites.Length; i++)
            {
                _defaultSpritesColors[i] = _allCharacterSprites[i].color;
            }
        }

        protected void BeatArrow(MusicArrow arrow)
        {
            arrow.Beat();
            
            OnBeatArrow?.Invoke(arrow.Type);
        }

        public void TakeDamage(Damage damage)
        {
            SetColor(_damagedColor);
            
            if(_currentResetColorRoutine != null)
                StopCoroutine(_currentResetColorRoutine);

            _currentResetColorRoutine = StartCoroutine(ResetDefaultColors());
        }

        private IEnumerator ResetDefaultColors()
        {
            yield return new WaitForSeconds(_resetColorsDelay);

            for (int i = 0; i < _allCharacterSprites.Length; i++)
            {
                _allCharacterSprites[i].color = _defaultSpritesColors[i];
            }
        }

        private void SetColor(Color color)
        {
            foreach (var sprite in _allCharacterSprites)
            {
                sprite.color = color;
            }
        }
    }
}