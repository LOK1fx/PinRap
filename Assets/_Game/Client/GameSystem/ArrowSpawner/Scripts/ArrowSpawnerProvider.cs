using UnityEngine;
using LOK1game.UI;

namespace LOK1game
{
    [RequireComponent(typeof(MusicTimeline))]
    public class ArrowSpawnerProvider : MonoBehaviour
    {
        private MusicTimeline _musicTimeline;
        private int _lastPosition = -1;
        private MusicData _musicData;

        private void Awake()
        {
            _musicTimeline = GetComponent<MusicTimeline>();
        }

        private void Start()
        {
            _musicTimeline.OnBeat += OnBeat;
            _musicTimeline.OnEndBeat += OnEndBeat;
            _musicTimeline.OnMusicStart += OnMusicStarted;
            _musicTimeline.OnMusicEnd += OmMusicEnded;
        }

        private void OmMusicEnded()
        {
            _musicData = null;
        }

        private void OnMusicStarted()
        {
            _musicData = _musicTimeline.MusicDataInstance;
        }

        private void OnBeat(MusicNode node)
        {
            if(_musicTimeline.GetCurrentPosition() == _lastPosition) { return; }
            
            var hud = PlayerHud.Instance;
            var data = new ArrowData()
            {
                Speed = _musicData.ArrowsBaseSpeed,
                Strength = node.BeatEffectStrength,
                Type = node.ArrowType,
            };
            
            if (node.Enemy)
            {
                hud.EnemyArrowSpawner.Spawn(data);
            }
            else
            {
                hud.PlayerArrowSpawner.Spawn(data);
            }

            _lastPosition = _musicTimeline.GetCurrentPosition();
        }

        private void OnEndBeat(MusicNode node)
        {
            
        }

        private void OnDestroy()
        {
            _musicTimeline.OnBeat -= OnBeat;
            _musicTimeline.OnEndBeat -= OnEndBeat;
            _musicTimeline.OnMusicStart -= OnMusicStarted;
            _musicTimeline.OnMusicEnd -= OmMusicEnded;
        }
    }
}