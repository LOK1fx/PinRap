using PlayerData.Manager;
using UnityEngine;

namespace CanvasScripts
{
    public class MainMenuRegistration : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController mainMenuCanvasController;
        private string _username;
        private string _password;
        private string _emailAddress;

        public void OnChangeUsername(string value)
        {
            _username = value;
        }
        
        public void OnChangePassword(string value)
        {
            _password = value;
        }
        public void OnChangeEmailAddress(string value)
        {
            _emailAddress = value;
        }

        public void CreateUser()
        {
            FlyingTextUIController.SpawnFlyingTextAndAfterDelete("Account is being created!");
            mainMenuCanvasController.NextMenuEvent();
            PlayerDataManager.CreateUser(_username, _emailAddress, _password, true);
            FlyingTextUIController.ActiveText.text = "Account created successfully!";
        }
    }
}