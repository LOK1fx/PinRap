using System;
using UnityEngine;

namespace LOK1game
{
    public abstract class Controller : MonoBehaviour
    {
        public event Action<IPawn> OnControlledPawnChanged;
        
        public IPawn ControlledPawn { get; private set; }

        protected abstract void Awake();
        protected abstract void Update();
        
        public static T Create<T>(IPawn pawn = null) where T : Controller
        {
            var controllerObject = new GameObject("[Controller]");
            var controller = controllerObject.AddComponent<T>();
            
            if(pawn != null)
                controller.SetControlledPawn(pawn);

            return controller;
        }
        
        public void SetControlledPawn(IPawn pawn)
        {
            ControlledPawn = pawn;
            ControlledPawn.OnPocces(this);
            
            OnControlledPawnChanged?.Invoke(pawn);
        }
    }
}