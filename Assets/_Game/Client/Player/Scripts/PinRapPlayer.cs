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
        }

        public override void OnInput(object sender)
        {
            _input.OnInput(sender);
        }

        public override void OnPocces(Controller sender)
        {
            _input.OnPocces(sender);
        }
    }
}