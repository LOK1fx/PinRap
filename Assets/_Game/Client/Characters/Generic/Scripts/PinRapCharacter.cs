using System;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game.PinRap
{
    public abstract class PinRapCharacter : Pawn
    {
        public event Action<EArrowType> OnBeatArrow;

        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;

        protected void BeatArrow(MusicArrow arrow)
        {
            arrow.Beat();
            
            OnBeatArrow?.Invoke(arrow.Type);
        }
    }
}