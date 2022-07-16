using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(PinRapPlayerInput))]
    public class PinRapPlayer : Actor
    {
        public PinRapPlayerInput Input { get; private set; }

        private void Awake()
        {
            Input = GetComponent<PinRapPlayerInput>();
        }
    }
}