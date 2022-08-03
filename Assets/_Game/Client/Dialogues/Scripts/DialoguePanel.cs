using System;
using System.Collections;
using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LOK1game
{
    public class DialoguePanel : MonoBehaviour
    {
        public event Action DialogueEntered;
        public static DialoguePanel Instance { get; private set; }

        public bool IsPlaying { get; private set; }

        [SerializeField] private float _typingSpeed = 0.1f;
        
        [Space]
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;
        
        private Story _currentStory;
        
        [Header("Character preview")]
        [SerializeField] private Image _speakerCharacter;

        private void Awake()
        {
            if(Instance != null)
                Destroy(gameObject);
            
            Instance = this;
            
            IsPlaying = false;
            _panel.SetActive(false);
            _speakerCharacter.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if(Input.GetButtonDown("Jump") && IsPlaying)
                if(_currentStory != null)
                    ContinueStory();
        }

        public void EnterDialogue(TextAsset inkJson, CharacterData speaker)
        {
            if(speaker.CanAppearInDialogues == false)
                EnterDialogue(inkJson);

            _speakerCharacter.sprite = speaker.DialogueCharacterSprite;
            _speakerCharacter.gameObject.SetActive(true);

            EnterDialogue(inkJson);
        }

        public void EnterDialogue(TextAsset inkJson)
        {
            _currentStory = new Story(inkJson.text);
            IsPlaying = true;
            _panel.SetActive(true);

            ContinueStory();
        }

        private void ExitDialogue()
        {
            _currentStory = null;
            IsPlaying = false;
            _panel.SetActive(false);
            _speakerCharacter.gameObject.SetActive(false);
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

        private IEnumerator DisplayLineRoutine(string text)
        {
            foreach (var character in text.ToCharArray())
            {
                _text.text += character;

                yield return new WaitForSeconds(_typingSpeed);
            }
        }
    }

}