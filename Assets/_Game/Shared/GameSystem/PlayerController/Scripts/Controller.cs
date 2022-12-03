using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LOK1game
{
    public abstract class Controller : MonoBehaviour
    {
        public event Action<IPawn> OnControlledPawnChanged;
        public IPawn ControlledPawn { get; private set; }

        private static List<Controller> _controllers = new List<Controller>();

        protected abstract void Awake();
        protected abstract void Update();
        
        public static T Create<T>(IPawn pawn = null) where T : Controller
        {
            var controllerObject = new GameObject($"{pawn}^Controller");
            var controller = controllerObject.AddComponent<T>();
            
            if(pawn != null)
                controller.SetControlledPawn(pawn);

            _controllers.Add(controller);
            
            return controller;
        }

        public static T GetController<T>() where T : Controller
        {
            if (TryGetController(out T controller))
                return controller;

            throw new InvalidCastException($"Where are no {nameof(T)} controller registred");
        }

        public static bool TryGetController<T>(out T foundController) where T : Controller
        {
            var controller = FindObjectOfType<T>();

            controller.TryGetComponent(out foundController);

            return true;
        }
        
        public void SetControlledPawn(IPawn pawn)
        {
            ControlledPawn = pawn;
            ControlledPawn.OnPocces(this);
            
            OnControlledPawnChanged?.Invoke(pawn);
        }
    }
}