using System;
using LOK1game.Game;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(PinRapPlayerInput))]
    public class PinRapPlayer : Pawn
    {
        [SerializeField] private CharacterData _character;
        
        private PinRapPlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PinRapPlayerInput>();
            
            SubscribeToEvents();
        }

        private void Start()
        {
            PlayerHud.Instance.DominationBar.SetPlayerCharacter(_character);
        }

        protected override void SubscribeToEvents()
        {
            _input.OnLeftArrowPressed += OnOnLeftArrowPressed;
            _input.OnDownArrowPressed += OnDownArrowPressed;
            _input.OnUpArrowPressed += OnUpArrowPressed;
            _input.OnRightArrowPressed += OnRightArrowPressed;
        }

        protected override void UnsubscribeFromEvents()
        {
            _input.OnLeftArrowPressed -= OnOnLeftArrowPressed;
            _input.OnDownArrowPressed -= OnDownArrowPressed;
            _input.OnUpArrowPressed -= OnUpArrowPressed;
            _input.OnRightArrowPressed -= OnRightArrowPressed;
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

        private void TryBeatArrow(BeatArrowChecker checker)
        {
            if (checker.IsArrowInbound(out var arrow))
            {
                arrow.Beat();
                
                PlayerHud.Instance.DominationBar.AddPoints(1);
                
                if(arrow.BeatEffectStrength != EBeatEffectStrength.None)
                    ClientApp.ClientContext.BeatController.InstantiateBeat(arrow.BeatEffectStrength);
            }
        }

        private UIArrowSpawner GetArrowSpawner()
        {
            return PlayerHud.Instance.PlayerArrowSpawner;
        }

        public override void OnInput(object sender)
        {
            if(ProjectContext.GetGameStateManager().CurrentGameState == EGameState.Paused) { return; }
            
            _input.OnInput(sender);
        }

        public override void OnPocces(Controller sender)
        {
            _input.OnPocces(sender);
        }
    }
}