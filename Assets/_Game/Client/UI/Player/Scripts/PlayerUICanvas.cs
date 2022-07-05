using UnityEngine;

namespace LOK1game.UI
{
    [RequireComponent(typeof(Canvas))]
    public class PlayerUICanvas : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}