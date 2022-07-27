using _Game.Client.Firebase;
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
            var text = FlyingTextUIController.Instance.SpawnFlyingTextAndAfterDelete("Account is being created!");
            mainMenuCanvasController.NextMenuEvent();
            FirebaseManager.CreateUser(_username, _emailAddress, _password, true);
            text.text = "Account created successfully!";
        }
    }
}