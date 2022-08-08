using System.Collections;
using _Game.Client.Firebase;
using Firebase.Auth;
using PlayerData.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasScripts
{
    public class MainMenuProfile : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController _mainMenuCanvasController;
        [SerializeField] private GameObject _noProfileContainer;
        [SerializeField] private GameObject _profileContainer;
        [SerializeField] private Text _playerName;
        [SerializeField] private Text _playerScore;
        [SerializeField] private Text _playerRating;
        [SerializeField] private Text _playerEmail;

        private void OnEnable()
        {
            if (!FirebaseManager.Authorized)
            {
                _profileContainer.SetActive(false);
                _noProfileContainer.SetActive(true);
            }
            else
            {
                StartCoroutine(LoadData());
            }
        }

        private IEnumerator LoadData()
        {
            if (FirebaseManager.CurrentPlayerData.Username == null)
            {
                _playerRating.text = "Loading...";
                _playerName.text = "Loading...";
                _playerScore.text = "Loading...";
                _playerEmail.text = "Loading...";
                yield return new WaitUntil(() => FirebaseManager.CurrentPlayerData.Username != null);
            }
            _playerRating.text = "" + FirebaseManager.CurrentPlayerData.GlobalRating;
            _playerName.text = FirebaseManager.CurrentPlayerData.Username;
            _playerScore.text = "" + FirebaseManager.CurrentPlayerData.GlobalScore;
            _playerEmail.text = FirebaseManager.CurrentPlayerData.EmailAddress;
        }

        public void QuitUserAccount()
        {
            _mainMenuCanvasController.NextMenuEvent();
            FlyingTextUIController.Instance.SpawnFlyingTextAndAfterDelete("You are logged out!");
            FirebaseManager.SignOut();
        }
    }
}