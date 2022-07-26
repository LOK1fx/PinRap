using LOK1game.Game;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(PinRapPlayerInput))]
    public class PinRapPlayer : Pawn
    {
        private PinRapPlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PinRapPlayerInput>();
            
            SubscribeToEvents();
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
            PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Right);
        }

        private void OnUpArrowPressed()
        {
            PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Up);
            
            Debug.Log(App.ProjectContext.GameSession.PlayTime);
        }

        private void OnDownArrowPressed()
        {
            PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Down);
        }

        private void OnOnLeftArrowPressed()
        {
            PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Left);
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