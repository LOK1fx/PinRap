using UnityEngine;

namespace _Game.Client.UI.CanvasMenuObjects.Scripts
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject startElementsContainer;
        [SerializeField] private GameObject backButton;
        private GameObject _activeMenu;

        private void Start()
        {
            _activeMenu = startElementsContainer;
        }

        public void SetBackButtonActive(bool active)
        {
            backButton.SetActive(active);
        }

        public void NextMenuEvent(GameObject nextMenu)
        {
            backButton.SetActive(true);
            _activeMenu.SetActive(false);
            nextMenu.SetActive(true);
            _activeMenu = nextMenu;
        }
    }
}
