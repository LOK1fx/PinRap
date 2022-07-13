using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(MusicTimeline))]
    public class MusicTimelineDebugVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject _beatIndicator;
        [SerializeField] private GameObject _musicStartedIndicator;
        [SerializeField] private GameObject _musicEndIndicator;

        private MusicTimeline _timeline;

        private void Awake()
        {
            _timeline = GetComponent<MusicTimeline>();

            _timeline.OnBeat += OnBeat;
            _timeline.OnEndBeat += OnOutOfBeat;
            _timeline.OnMusicStart += OnMusicStart;
            _timeline.OnMusicEnd += OnMusicEnd;

            _beatIndicator.SetActive(false);
            _musicStartedIndicator.SetActive(false);
            _musicEndIndicator.SetActive(false);
        }

        private void OnMusicStart()
        {
            _musicStartedIndicator.SetActive(true);
        }

        private void OnMusicEnd()
        {
            _musicEndIndicator.SetActive(true);
        }

        private void OnBeat(MusicNode node)
        {
            _beatIndicator.SetActive(true);

            Debug.Log("On beat");
        }

        private void OnOutOfBeat(MusicNode node)
        {
            _beatIndicator.SetActive(false);

            Debug.Log("Out of beat");
        }

        private void OnDestroy()
        {
            _timeline.OnBeat -= OnBeat;
            _timeline.OnEndBeat -= OnOutOfBeat;
            _timeline.OnMusicStart -= OnMusicStart;
            _timeline.OnMusicEnd -= OnMusicEnd;
        }
    }
}