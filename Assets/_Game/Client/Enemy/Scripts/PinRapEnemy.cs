using System;
using System.Collections;
using LOK1game.PinRap;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    public class PinRapEnemy : PinRapCharacter
    {
        public event Action OnSuccesfullBeat;

        [Header("PinRapEnemy: dialogues")]
        [SerializeField] private TextAsset _startDialogue;
        [SerializeField] private float _startDialogueDelay = 1f;
        [SerializeField] private TextAsset _endDialogue;

        private UIArrowSpawner _arrowSpawner;
        private MusicTimeline _musicTimeline;
        private Coroutine _currentDialogueRoutine;

        private void Start()
        {
            if (_startDialogue != null)
            {
                if(_currentDialogueRoutine != null)
                    StopCoroutine(_currentDialogueRoutine);
                
                _currentDialogueRoutine = StartCoroutine(StartDialogue(_startDialogue, _startDialogueDelay));
            }

            if (_endDialogue == null) return;
            
            _musicTimeline = MusicTimeline.Instance;
            _musicTimeline.OnMusicEnd += OnMusicEnd;
        }

        private void OnDestroy()
        {
            if(_musicTimeline == null) { return; }

            _musicTimeline.OnMusicEnd -= OnMusicEnd;
        }

        private void OnMusicEnd()
        {
            if(_currentDialogueRoutine != null)
                StopCoroutine(_currentDialogueRoutine);
            
            _currentDialogueRoutine = StartCoroutine(StartDialogue(_endDialogue,0f));
            DialoguePanel.Instance.DialogueEnded.AddListener(() => TransitionLoad.LoadScene("PinRapMainMenu"));
        }

        private IEnumerator StartDialogue(TextAsset dialogueAsset, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            DialoguePanel.Instance.EnterDialogue(dialogueAsset, CharacterData);
        }

        public override void OnPocces(Controller sender)
        {
            base.OnPocces(sender);

            var controller = Controller as PinRapEnemyController;

            _arrowSpawner = controller.ArrowSpawner;
        }

        public override void OnUnpocces()
        {
            base.OnUnpocces();

            _arrowSpawner = null;
        }

        public override void OnInput(object sender)
        {
            if(_arrowSpawner == null) { return; }
            
            TryBeatArrow(_arrowSpawner.DownArrowChecker);
            TryBeatArrow(_arrowSpawner.LeftArrowChecker);
            TryBeatArrow(_arrowSpawner.UpArrowChecker);
            TryBeatArrow(_arrowSpawner.RightArrowChecker);
        }
        
        private void TryBeatArrow(MusicArrowChecker checker)
        {
            if (checker.IsArrowInbound(out var arrow))
            {
                BeatArrow(arrow);

                OnSuccesfullBeat?.Invoke();
            }
        }
    }
}