using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        [SerializeField] private float _moveSpeed;

        private Vector3 _destination;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(Position, _destination, Time.deltaTime * _moveSpeed);
        }

        public void Setup(Vector3 destination)
        {
            _destination = destination;
        }
    }
}