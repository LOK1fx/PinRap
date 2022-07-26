using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(BeatsPerMinuteCounter))]
    public class BPMWorldSetter : MonoBehaviour
    {
        private BeatsPerMinuteCounter _counter;

        private void Start()
        {
            _counter = GetComponent<BeatsPerMinuteCounter>();

            //_counter.CurrentBPM = GameWorld.Current.GetBPM(); //too strange to be alive
        }
    }
}