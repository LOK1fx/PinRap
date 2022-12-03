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
        public event Action OnPointsRefreshed;

        public PinRapPlayerInput Input { get; private set; }

        [SerializeField] private Vector3 _popupTextOffset;

        private bool _isDead;

        protected override void Awake()
        {
            base.Awake();
            
            Input = GetComponent<PinRapPlayerInput>();
            SubscribeToEvents();
        }

        public void Kill()
        {
            _isDead = true;

            TransitionLoad.LoadScene("PinRapMainMenu");
        }

        public override void OnPocces(Controller sender)
        {
            LocalPlayer.Initialize(this, sender, World);
            LocalPlayer.Controller.Points = World.LevelConfigData.StartPlayerPoints;

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
                var earnPoints = World.LevelConfigData.EarnPoints;
                popupText = new PopupTextParams(World.LevelConfigData.EarnPoints.ToString(), 3f);

                LocalPlayer.AddPoints();
                BeatArrow(arrow);

                if (arrow.BeatEffectStrength != EBeatEffectStrength.None)
                    ClientApp.ClientContext.BeatController.InstantiateBeat(arrow.BeatEffectStrength);
            }
            else
            {
                var losedPoints = World.LevelConfigData.LosedPoints;
                popupText = new PopupTextParams($"-{losedPoints}", 3f, Color.red);

                LocalPlayer.RemovePoints();
                TakeDamage(new Damage(5));
            }

            var popup = PopupText.Spawn<PopupText3D>(transform.position, popupText);
            popup.SetPosition(popupPosition);

            PointsChanged();
        }

        public void PointsChanged()
        {
            OnPointsRefreshed?.Invoke();

            if (LocalPlayer.Controller.Points <= 0)
            {
                MusicTimeline.Instance.StopPlayback();

                Kill();
            }
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