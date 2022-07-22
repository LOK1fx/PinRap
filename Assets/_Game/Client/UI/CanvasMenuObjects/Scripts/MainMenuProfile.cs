using System.Collections;
using Firebase.Auth;
using PlayerData.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasScripts
{
    public class MainMenuProfile : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController mainMenuCanvasController;
        public GameObject regContainer;
        public Text playerName;
        public Text playerScore;
        public Text playerRating;
        public Text playerEmail;

        private void OnEnable()
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser == null)
            {
                mainMenuCanvasController.NextMenuEvent(regContainer);
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(LoadData());
            }
        }

        private IEnumerator LoadData()
        {
            if (PlayerDataManager.CurrentPlayerData.Username == null)
            {
                playerRating.text = "Loading...";
                playerName.text = "Loading...";
                playerScore.text = "Loading...";
                playerEmail.text = "Loading...";
                yield return new WaitUntil(() => PlayerDataManager.CurrentPlayerData.Username != null);
            }
            playerRating.text = "" + PlayerDataManager.CurrentPlayerData.GlobalRating;
            playerName.text = PlayerDataManager.CurrentPlayerData.Username;
            playerScore.text = "" + PlayerDataManager.CurrentPlayerData.GlobalScore;
            playerEmail.text = PlayerDataManager.CurrentPlayerData.EmailAddress;
        }

        public void QuitUserAccount()
        {
            mainMenuCanvasController.NextMenuEvent();
            FlyingTextUIController.SpawnFlyingTextAndAfterDelete("Account is being created!");
            FirebaseAuth.DefaultInstance.SignOut();
            FlyingTextUIController.ActiveText.text = "Account created successfully!";
        }
    }
}