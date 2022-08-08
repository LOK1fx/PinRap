using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(MusicTimeline))]
    public class Finisher : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad;
        
        private MusicTimeline _timeline;

        private void Awake()
        {
            _timeline = GetComponent<MusicTimeline>();
            _timeline.OnMusicEnd += OnMusicEnded;
        }

        private void OnMusicEnded()
        {
            TransitionLoad.LoadLevel(_levelToLoad);
        }

        private void OnDestroy()
        {
            _timeline.OnMusicEnd -= OnMusicEnded;
        }
    }
}