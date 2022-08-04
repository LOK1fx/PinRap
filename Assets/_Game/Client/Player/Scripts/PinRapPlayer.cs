using System;
using LOK1game.Game;
using LOK1game.PinRap;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(PinRapPlayerInput))]
    public class PinRapPlayer : PinRapCharacter
    {
        public event Action OnDie;
        public PinRapPlayerInput Input { get; private set; }

        [SerializeField] private Vector3 _popupTextOffset;
        
        private bool _isDead;

        private void Awake()
        {
            Input = GetComponent<PinRapPlayerInput>();
            
            SubscribeToEvents();
        }

        private void Start()
        {
            PlayerHud.Instance.DominationBar.SetPlayerCharacter(CharacterData);
            PlayerHud.Instance.DominationBar.OnPointsChanged += OnDominationBarPointsChanged;
            
            LocalPlayer.Initialize(this);
        }

        private void OnDominationBarPointsChanged(int points)
        {
            if(points > 0) { return; }
            
            MusicTimeline.Instance.StopPlayback();
            
            Kill();
        }

        protected override void SubscribeToEvents()
        {
            Input.OnLeftArrowPressed += OnOnLeftArrowPressed;
            Input.OnDownArrowPressed += OnDownArrowPressed;
            Input.OnUpArrowPressed += OnUpArrowPressed;
            Input.OnRightArrowPressed += OnRightArrowPressed;
        }

        public void Kill()
        {
            _isDead = true;
            
            OnDie?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            Input.OnLeftArrowPressed -= OnOnLeftArrowPressed;
            Input.OnDownArrowPressed -= OnDownArrowPressed;
            Input.OnUpArrowPressed -= OnUpArrowPressed;
            Input.OnRightArrowPressed -= OnRightArrowPressed;
        }

        private void OnRightArrowPressed()
        {
            TryBeatArrow(GetArrowSpawner().RightArrowChecker);
        }

        private void OnUpArrowPressed()
        {
            TryBeatArrow(GetArrowSpawner().UpArrowChecker);
        }

        private void OnDownArrowPressed()
        {
            TryBeatArrow(GetArrowSpawner().DownArrowChecker);
        }

        private void OnOnLeftArrowPressed()
        {
            TryBeatArrow(GetArrowSpawner().LeftArrowChecker);
        }

        private void TryBeatArrow(MusicArrowChecker checker)
        {
            var popupPosition = transform.position + _popupTextOffset;
            
            if (checker.IsArrowInbound(out var arrow))
            {
                BeatArrow(arrow);
                
                PlayerHud.Instance.DominationBar.AddPoints(1);

                var text = new PopupTextParams("1", 3f);
                var popup = PopupText.Spawn<PopupText3D>(transform.position, text);
                popup.SetPosition(popupPosition);
                
                if(arrow.BeatEffectStrength != EBeatEffectStrength.None)
                    ClientApp.ClientContext.BeatController.InstantiateBeat(arrow.BeatEffectStrength);
            }
            else
            {
                PlayerHud.Instance.DominationBar.RemovePoints(5);
                
                var text = new PopupTextParams("-5", 3f);
                var popup = PopupText.Spawn<PopupText3D>(transform.position, text);
                popup.SetPosition(popupPosition);
            }
        }

        private UIArrowSpawner GetArrowSpawner()
        {
            return PlayerHud.Instance.PlayerArrowSpawner;
        }

        public override void OnInput(object sender)
        {
            var currentGameState = ProjectContext.GetGameStateManager().CurrentGameState;
            
            if(currentGameState == EGameState.Paused || _isDead || DialoguePanel.Instance.IsPlaying) { return; }
            
            Input.OnInput(sender);
        }

        public override void OnPocces(Controller sender)
        {
            Input.OnPocces(sender);
        }
    }
}