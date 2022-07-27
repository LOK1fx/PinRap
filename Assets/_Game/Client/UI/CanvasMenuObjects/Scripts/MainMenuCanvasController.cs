using UnityEngine;

namespace CanvasScripts
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _startElementsContainer;
        [SerializeField] private GameObject _backButton;
        
        internal GameObject ActiveMenu;

        private void Start()
        {
            FlyingTextUIController.CanvasTransform = transform;
            ActiveMenu = _startElementsContainer;
        }

        public void SetBackButtonActive(bool active)
        {
            _backButton.SetActive(active);
        }

        public void NextMenuEvent(GameObject nextMenu)
        {
            _backButton.SetActive(true);
            ActiveMenu.SetActive(false);
            nextMenu.SetActive(true);
            ActiveMenu = nextMenu;
        }
        
        public void NextMenuEvent()
        {
            _backButton.SetActive(false);
            ActiveMenu.SetActive(false);
            _startElementsContainer.SetActive(true);
            ActiveMenu = _startElementsContainer;
        }

#if UNITY_EDITOR
        public void SpawnFlyingTextTest()
        {
            FlyingTextUIController.Instance.SpawnFlyingTextAndAfterDelete("Account is being created!");
        }
#endif
    }
}
