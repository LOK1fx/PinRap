using UnityEngine;
using LOK1game.UI;

namespace LOK1game
{
    [RequireComponent(typeof(MusicTimeline))]
    public class ArrowSpawnerProvider : MonoBehaviour
    {
        private MusicTimeline _musicTimeline;
        private int _lastPosition = -1;

        private void Awake()
        {
            _musicTimeline = GetComponent<MusicTimeline>();
        }

        private void Start()
        {
            _musicTimeline.OnBeat += OnBeat;
            _musicTimeline.OnEndBeat += OnEndBeat;
        }
        
        private void OnBeat(MusicNode node)
        {
            if(_musicTimeline.GetCurrentPosition() == _lastPosition) { return; }

            var hud = PlayerHud.Instance;
            
            if (node.Enemy)
            {
                hud.EnemyArrowSpawner.Spawn(node.ArrowType, node.BeatEffectStrength);
            }
            else
            {
                hud.PlayerArrowSpawner.Spawn(node.ArrowType, EBeatEffectStrength.None);
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
        }
    }
}