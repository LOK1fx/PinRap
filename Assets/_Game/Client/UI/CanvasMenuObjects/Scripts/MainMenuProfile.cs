using System.Collections;
using Firebase.Auth;
using PlayerData.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasScripts
{
    public class MainMenuProfile : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController _mainMenuCanvasController;
        [SerializeField] private GameObject _regContainer;
        [SerializeField] private Text _playerName;
        [SerializeField] private Text _playerScore;
        [SerializeField] private Text _playerRating;
        [SerializeField] private Text _playerEmail;

        private void OnEnable()
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser == null)
            {
                _mainMenuCanvasController.NextMenuEvent(_regContainer);
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
                _playerRating.text = "Loading...";
                _playerName.text = "Loading...";
                _playerScore.text = "Loading...";
                _playerEmail.text = "Loading...";
                yield return new WaitUntil(() => PlayerDataManager.CurrentPlayerData.Username != null);
            }
            _playerRating.text = "" + PlayerDataManager.CurrentPlayerData.GlobalRating;
            _playerName.text = PlayerDataManager.CurrentPlayerData.Username;
            _playerScore.text = "" + PlayerDataManager.CurrentPlayerData.GlobalScore;
            _playerEmail.text = PlayerDataManager.CurrentPlayerData.EmailAddress;
        }

        public void QuitUserAccount()
        {
            _mainMenuCanvasController.NextMenuEvent();
            FlyingTextUIController.SpawnFlyingTextAndAfterDelete("Account is being created!");
            FirebaseAuth.DefaultInstance.SignOut();
            FlyingTextUIController.ActiveText.text = "Account created successfully!";
        }
    }
}