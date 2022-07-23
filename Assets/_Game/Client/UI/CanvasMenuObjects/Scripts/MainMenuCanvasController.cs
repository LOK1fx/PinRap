using UnityEngine;

namespace CanvasScripts
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _startElementsContainer;
        [SerializeField] private GameObject _backButton;
        
        private GameObject _activeMenu;

        private void Start()
        {
            FlyingTextUIController.CanvasTransform = transform;
            _activeMenu = _startElementsContainer;
        }

        public void SetBackButtonActive(bool active)
        {
            _backButton.SetActive(active);
        }

        public void NextMenuEvent(GameObject nextMenu)
        {
            _backButton.SetActive(true);
            _activeMenu.SetActive(false);
            nextMenu.SetActive(true);
            _activeMenu = nextMenu;
        }
        
        public void NextMenuEvent()
        {
            _backButton.SetActive(false);
            _activeMenu.SetActive(false);
            _startElementsContainer.SetActive(true);
            _activeMenu = _startElementsContainer;
        }
    }
}
