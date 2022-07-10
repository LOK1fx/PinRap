using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LOK1game
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class BeatEffectController : PersistentSingleton<BeatEffectController>
    {
        [SerializeField] private float _coolSpeed = 8f;

        private PostProcessVolume _volume;

        protected override void Awake()
        {
            base.Awake();

            _volume = GetComponent<PostProcessVolume>();
            _volume.weight = 0f;
        }

        private void Update()
        {
            _volume.weight = Mathf.Lerp(_volume.weight, 0f, Time.deltaTime * _coolSpeed);
        }

        private void LateUpdate()
        {
            _volume.weight = Mathf.Clamp01(_volume.weight); //fixs the strange grey screen that appears some times
        }

        public void InstantiateBeat(EBeatEffectStrength strength)
        {
            _volume.weight = 1f / (int)strength;

            Debug.Log(_volume.weight);
        }
    }


    public enum EBeatEffectStrength : uint
    {
        Weak = 1,
        Medium,
        Strong,
    }
}