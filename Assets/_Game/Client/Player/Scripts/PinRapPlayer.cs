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

        protected override void Awake()
        {
            base.Awake();
            
            Input = GetComponent<PinRapPlayerInput>();
            SubscribeToEvents();
        }

        private void Start()
        {
            PlayerHud.Instance.DominationBar.SetPlayerCharacter(CharacterData);
            PlayerHud.Instance.DominationBar.OnPointsChanged += OnDominationBarPointsChanged;
            
            LocalPlayer.Initialize(this);
        }

        public void Kill()
        {
            _isDead = true;

            TransitionLoad.LoadScene("PinRapMainMenu");
        }

        public override void OnPocces(Controller sender)
        {
            Input.OnPocces(sender);
        }

        public override void OnInput(object sender)
        {
            var currentGameState = GetProjectContext().GameStateManager.CurrentGameState;

            if (currentGameState == EGameState.Paused || _isDead || DialoguePanel.Instance.IsPlaying)
                return;

            Input.OnInput(sender);
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
            var popupText = new PopupTextParams("None", Color.white);

            if (checker.IsArrowInbound(out var arrow))
            {
                PlayerHud.Instance.DominationBar.AddPoints(1);
                popupText = new PopupTextParams("1", 3f);

                BeatArrow(arrow);

                if (arrow.BeatEffectStrength != EBeatEffectStrength.None)
                    ClientApp.ClientContext.BeatController.InstantiateBeat(arrow.BeatEffectStrength);
            }
            else
            {
                PlayerHud.Instance.DominationBar.RemovePoints(5);
                popupText = new PopupTextParams("-5", 3f);

                TakeDamage(new Damage(5));
            }

            var popup = PopupText.Spawn<PopupText3D>(transform.position, popupText);
            popup.SetPosition(popupPosition);
        }

        private void OnDominationBarPointsChanged(int points)
        {
            if (points > 0) { return; }

            MusicTimeline.Instance.StopPlayback();

            Kill();
        }

        private UIArrowSpawner GetArrowSpawner()
        {
            return PlayerHud.Instance.PlayerArrowSpawner;
        }

        protected override void SubscribeToEvents()
        {
            Input.OnLeftArrowPressed += OnOnLeftArrowPressed;
            Input.OnDownArrowPressed += OnDownArrowPressed;
            Input.OnUpArrowPressed += OnUpArrowPressed;
            Input.OnRightArrowPressed += OnRightArrowPressed;
        }

        protected override void UnsubscribeFromEvents()
        {
            Input.OnLeftArrowPressed -= OnOnLeftArrowPressed;
            Input.OnDownArrowPressed -= OnDownArrowPressed;
            Input.OnUpArrowPressed -= OnUpArrowPressed;
            Input.OnRightArrowPressed -= OnRightArrowPressed;
        }
    }
}