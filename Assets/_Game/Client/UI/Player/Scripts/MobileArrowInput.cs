using UnityEngine;

namespace LOK1game.PinRap.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MobileArrowInput : MonoBehaviour
    {
        private CanvasGroup _canvas;
        
        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
       
#if UNITY_STANDALONE
            Deactivate();
#endif
#if UNITY_EDITOR
            Activate();
#endif
#if UNITY_ANDROID
            Activate();
#endif
        }

        private void Activate()
        {
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
            _canvas.alpha = 1f;
        }

        private void Deactivate()
        {
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
            _canvas.alpha = 0f;
        }

        public void Left()
        {
            LocalPlayer.Input.PressLeft();
        }

        public void Down()
        {
            LocalPlayer.Input.PressDown();
        }

        public void Up()
        {
            LocalPlayer.Input.PressDown();
        }

        public void Right()
        {
            LocalPlayer.Input.PressRight();
        }
    }
}