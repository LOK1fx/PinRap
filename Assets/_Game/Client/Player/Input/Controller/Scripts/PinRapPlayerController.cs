using System;
using UnityEngine;

namespace LOK1game
{
    public class PinRapPlayerController : PlayerController
    {
        public event Action ContinueDialogueButtonPressed;
        
        protected override void Update()
        {
            base.Update();

            if (Input.GetButton("Jump"))
            {
                ContinueDialogueButtonPressed?.Invoke();
            }
        }
    }
}