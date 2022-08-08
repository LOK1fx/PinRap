using LOK1game.PinRap;
using UnityEngine;

namespace LOK1game.World
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float _strength = 0.1f;
        private Vector3 _cameraPreviousPosition;
        
        private void LateUpdate()
        {
            var camera = LocalPlayer.Camera.transform;
            
            if (camera == null)
                return;
            
            var delta = camera.position - _cameraPreviousPosition;

            _cameraPreviousPosition = camera.position;
            transform.position += delta * _strength;
        }
    }
}