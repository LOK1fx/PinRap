using UnityEngine;

namespace CanvasScripts
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject startElementsContainer;
        [SerializeField] private GameObject backButton;
        internal GameObject ActiveMenu;

        private void Start()
        {
            FlyingTextUIController.CanvasTransform = transform;
            ActiveMenu = startElementsContainer;
        }

        public void SetBackButtonActive(bool active)
        {
            backButton.SetActive(active);
        }

        public void NextMenuEvent(GameObject nextMenu)
        {
            backButton.SetActive(true);
            ActiveMenu.SetActive(false);
            nextMenu.SetActive(true);
            ActiveMenu = nextMenu;
        }
        
        public void NextMenuEvent()
        {
            backButton.SetActive(false);
            ActiveMenu.SetActive(false);
            startElementsContainer.SetActive(true);
            ActiveMenu = startElementsContainer;
        }
    }
}
