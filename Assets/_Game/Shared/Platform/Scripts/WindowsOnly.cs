using UnityEngine;

namespace LOK1game.Platform
{
    public class WindowsOnly : MonoBehaviour
    {
        [SerializeField] private bool _ativeInEditor = true;
        
        private void Start()
        {
#if UNITY_ANDROID
            
            gameObject.SetActive(false);
            
#endif
            
#if UNITY_EDITOR
            
            if(_ativeInEditor)
                gameObject.SetActive(true);
            
#endif
        }
    }
}