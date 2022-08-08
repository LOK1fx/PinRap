using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LOK1game
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class PostProcessingVolumeActivitySetter : MonoBehaviour
    {
        private PostProcessVolume _volume;
        
        private void Awake()
        {
            _volume = GetComponent<PostProcessVolume>();
            _volume.enabled = !PostProcessingController.IsTurnedOff;
        }
    }
}