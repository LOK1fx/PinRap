using UnityEngine;
using System;

namespace LOK1game
{
    public class PinRapPlayerInput : MonoBehaviour, IPawn
    {
        #region Events

        public event Action OnLeftArrowPressed;
        public event Action OnUpArrowPressed;
        public event Action OnDownArrowPressed;
        public event Action OnRightArrowPressed;
        public event Action OnPoccesed;

        #endregion
        
        public Controller Controller { get; private set; }
        
        //Temp keys, after we just make a mobile and pc input provider
        [SerializeField] private KeyCode _tryBeatLeftArrowKey = KeyCode.A;
        [SerializeField] private KeyCode _tryBeatUpArrowKey = KeyCode.W;
        [SerializeField] private KeyCode _tryBeatDownArrowKey = KeyCode.S;
        [SerializeField] private KeyCode _tryBeatRightArrowKey = KeyCode.D;

        [SerializeField] private KeyCode _instantiateBeatKey = KeyCode.G; //Just for testing
        
        public void OnPocces(Controller sender)
        {
            Controller = sender;

            OnPoccesed?.Invoke();
            
            //Maybe do a subscription to mobile input provider
        }

        public void OnUnpocces()
        {
            Controller = null;
        }

        //Will called from PinRapPlayerController
        //Calls only when our player is under control
        public void OnInput(object sender)
        {
            if(Input.GetKeyDown(_instantiateBeatKey))
                ClientApp.ClientContext.BeatController.InstantiateBeat(EBeatEffectStrength.Medium); //Just for testing
            
            Handlekey(_tryBeatLeftArrowKey, OnLeftArrowPressed);
            Handlekey(_tryBeatUpArrowKey, OnUpArrowPressed);
            Handlekey(_tryBeatDownArrowKey, OnDownArrowPressed);
            Handlekey(_tryBeatRightArrowKey, OnRightArrowPressed);
        }

        private void Handlekey(KeyCode key, Action callback)
        {
            if(Input.GetKeyDown(key))
                callback?.Invoke();
        }
    }
}