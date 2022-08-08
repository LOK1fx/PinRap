using UnityEngine;

namespace LOK1game
{
    public class PostProcessingController
    {
        public delegate void PostProcessingControllerState(bool turnedOff);
        public event PostProcessingControllerState OnStateChanged;
        public static bool IsTurnedOff { get; private set; }

        public void UpdateState()
        {
            if (PlayerPrefs.HasKey("TurnOffPP") == false)
            {
                SetState(false);
                return;
            }

            var savedValue = PlayerPrefs.GetInt("TurnOffPP");
            SetState(savedValue == 1);
        }

        private void SetState(bool state)
        {
            IsTurnedOff = state;
            OnStateChanged?.Invoke(IsTurnedOff);
        }
    }
}