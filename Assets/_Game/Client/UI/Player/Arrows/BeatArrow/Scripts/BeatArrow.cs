using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            transform.localPosition += Vector3.up * Time.deltaTime * moveSpeed;
        }
    }
}