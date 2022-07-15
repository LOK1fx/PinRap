using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(BeatsPerMinuteCounter))]
    public class PermomentBeat : MonoBehaviour
    {
        [SerializeField] private EBeatEffectStrength _beatEffectStrength = EBeatEffectStrength.Weak;

        private BeatsPerMinuteCounter _bpmCounter;

        private void Awake()
        {
            _bpmCounter = GetComponent<BeatsPerMinuteCounter>();
            _bpmCounter.OnBeat += OnBeat;
        }

        private void OnBeat()
        {
            ClientApp.ClientContext.BeatController.InstantiateBeat(_beatEffectStrength);
        }

        private void OnDestroy()
        {
            _bpmCounter.OnBeat -= OnBeat;
        }
    }
}