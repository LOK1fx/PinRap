using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        public float moveSpeed;

        private void Update()
        {
            transform.localPosition += Vector3.up * Time.deltaTime * moveSpeed;
        }
    }
}