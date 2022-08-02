using System;
using System.Collections;
using System.Collections.Generic;
using LOK1game;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.PinRap
{
    public class PinRapTutorialEnemy : PinRapEnemy
    {
        [SerializeField] private TextAsset _dialogueInkAsset;
        
        private void Awake()
        {
            DialoguePanel.Instance.EnterDialogue(_dialogueInkAsset);
        }
    }
}