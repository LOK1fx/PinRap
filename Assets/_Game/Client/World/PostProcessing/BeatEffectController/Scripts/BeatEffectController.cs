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

        [ContextMenu("Beat")]
        public void InstantiateBeat()
        {
            _volume.weight = 1f;
        }
    }

}