using System;
using UnityEngine;

namespace LOK1game
{
    public abstract class Controller : MonoBehaviour
    {
        public event Action<IPawn> OnControlledPawnChanged;
        
        public IPawn ControlledPawn { get; private set; }

        protected abstract void Update();

        public void SetControlledPawn(IPawn pawn)
        {
            ControlledPawn = pawn;
            ControlledPawn.OnPocces(this);
            
            OnControlledPawnChanged?.Invoke(pawn);
        }
    }
}