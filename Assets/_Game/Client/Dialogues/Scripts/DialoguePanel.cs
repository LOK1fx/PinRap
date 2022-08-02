using System;
using Ink.Runtime;
using UnityEngine;
using TMPro;

namespace LOK1game
{
    public class DialoguePanel : MonoBehaviour
    {
        public event Action DialogueEntered;
        public static DialoguePanel Instance { get; private set; }

        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;

        private Story _currentStory;
        private bool _dialogueIsPlaying;

        private PinRapPlayerController _controller;

        private void Awake()
        {
            if(Instance != null)
                Destroy(gameObject);
            
            Instance = this;
            
            _dialogueIsPlaying = false;
            _panel.SetActive(false);

            if(Controller.TryGetController(out _controller))
                _controller.ContinueDialogueButtonPressed += ContinueStory;
        }

        private void OnDestroy()
        {
            Instance = null;

            if(_controller != null)
                _controller.ContinueDialogueButtonPressed -= ContinueStory;
        }

        public void EnterDialogue(TextAsset inkJson)
        {
            _currentStory = new Story(inkJson.text);
            _dialogueIsPlaying = true;
            _panel.SetActive(true);

            ContinueStory();
        }

        private void ExitDialogue()
        {
            _currentStory = null;
            _dialogueIsPlaying = false;
            _panel.SetActive(false);
        }

        private void ContinueStory()
        {
            if (_currentStory.canContinue)
            {
                _text.text = _currentStory.Continue();
                
                DialogueEntered?.Invoke();
            }
            else
            {
                ExitDialogue();
            }
        }
    }

}