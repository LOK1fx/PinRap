using System.Collections;
using _Game.Client.Firebase;
using UnityEngine;

namespace CanvasScripts
{
    public class MainMenuAuthorization : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController mainMenuCanvasController;
        private string _password;
        private string _emailAddress;
        private bool _authEnd;
    
        public void OnChangePassword(string value)
        {
            _password = value;
        }
        public void OnChangeEmailAddress(string value)
        {
            _emailAddress = value;
        }

        public void AuthUser()
        {
            FirebaseManager.AuthUser(_emailAddress, _password, b =>
            {
                _authEnd = true;
            });
            FlyingTextUIController.Instance.SpawnFlyingText("Loading...", StartWaitToAuth);
        }

        private void StartWaitToAuth(Transform activeText)
        {
            StartCoroutine(WaitToAuth(activeText));
        }
        
        private IEnumerator WaitToAuth(Transform activeText)
        {
            yield return new WaitUntil(() => _authEnd);
            FlyingTextUIController.Instance.MoveToEndAndDie(activeText, 0);
            FlyingTextUIController.Instance.SpawnFlyingTextAndAfterDelete(
                FirebaseManager.Authorized 
                    ? "You have logged in successfully!" 
                    : "An error has occurred, you may have entered incorrect data!");
            mainMenuCanvasController.NextMenuEvent();
        }
    }
}
