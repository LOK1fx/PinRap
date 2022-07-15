using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        [SerializeField] private float _moveSpeed;

        private Vector3 _destination;

        private void Update()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination, Time.deltaTime * _moveSpeed);
        }

        public void Setup(Vector3 destination)
        {
            _destination = destination;
        }
    }
}