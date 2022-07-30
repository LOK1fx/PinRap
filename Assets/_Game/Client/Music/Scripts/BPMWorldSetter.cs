using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(BeatsPerMinuteCounter))]
    public class BPMWorldSetter : MonoBehaviour
    {
        private BeatsPerMinuteCounter _counter;
        private MusicTimeline _timeline;

        private void Start()
        {
            _counter = GetComponent<BeatsPerMinuteCounter>();

            _timeline = MusicTimeline.Instance;
            _timeline.OnMusicStart += OnMusicStarted;
        }

        private void OnMusicStarted()
        {
            var musicData = _timeline.MusicDataInstance;

            _counter.SetBPM(musicData.BPM);
        }
    }
}