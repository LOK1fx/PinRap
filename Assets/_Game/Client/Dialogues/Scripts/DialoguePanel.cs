using System;
using System.Collections;
using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LOK1game
{
    public class DialoguePanel : MonoBehaviour
    {
        public UnityEvent DialogueEntered;
        public UnityEvent DialogueEnded;
        
        public static DialoguePanel Instance { get; private set; }

        public bool IsPlaying { get; private set; }

        [Space]
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;
        
        private Story _currentStory;
        
        [Header("Character preview")]
        [SerializeField] private Image _speakerCharacter;
        
        [Header("Typing effect")]
        [SerializeField] private float _typingSpeed = 0.1f;
        [SerializeField] private AudioClip _typeClip;
        [SerializeField] private AudioClip _continuieClip;
        [SerializeField] private AudioSource _audio;

        private Coroutine _displayLineRoutine;

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
            if (Input.GetButtonDown("Jump") && IsPlaying)
            {
                if (_currentStory != null)
                {
                    _audio.PlayOneShot(_continuieClip);

                    ContinueStory();
                }
            }
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
            
            DialogueEntered?.Invoke();
        }

        private void ExitDialogue()
        {
            _currentStory = null;
            IsPlaying = false;
            _panel.SetActive(false);
            _speakerCharacter.gameObject.SetActive(false);
            _audio.Stop();
            
            DialogueEnded?.Invoke();
        }

        public void ContinueStory()
        {
            if (_currentStory.canContinue)
            {
                if (_displayLineRoutine != null)
                    StopCoroutine(_displayLineRoutine);
                
                _displayLineRoutine = StartCoroutine(DisplayLineRoutine(_currentStory.Continue()));
            }
            else
            {
                ExitDialogue();
            }
        }

        private IEnumerator DisplayLineRoutine(string text)
        {
            _text.text = "";
            
            foreach (var character in text.ToCharArray())
            {
                _text.text += character;
                _audio.PlayOneShot(_typeClip);

                yield return new WaitForSeconds(_typingSpeed);
            }
        }
    }

}