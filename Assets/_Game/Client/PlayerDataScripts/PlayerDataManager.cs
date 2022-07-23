using System.Linq;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

namespace PlayerData.Manager
{
    public class PlayerDataManager : MonoBehaviour
    {
        public static PlayerData CurrentPlayerData;
        public static FirebaseApp FirebaseApp;
        public static FirebaseFirestore FirebaseFirestore;
        public static FirebaseAuth FirebaseAuth;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (task.Result == DependencyStatus.Available)
                {
                    InitializationFirebase();
                    Debug.Log(FirebaseApp.Options.AppId);
                } 
                else 
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
            
        }

        public static void CreateUser(string name, string email, string password, bool isCurrentPlayer)
        {
            FirebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(result =>
            {
                var playerData = new PlayerData {Username = name, EmailAddress = email};
                if (isCurrentPlayer)
                {
                    CurrentPlayerData = playerData;
                }
                FirebaseFirestore.Collection("users").Document(result.Result.UserId)
                    .SetAsync(playerData);
            });
        }

        private static void InitializationFirebase()
        {
            FirebaseApp = FirebaseApp.DefaultInstance;
            FirebaseFirestore = FirebaseFirestore.DefaultInstance;
            FirebaseAuth = FirebaseAuth.DefaultInstance;
            if (FirebaseAuth.CurrentUser != null)
            {
                FirebaseFirestore.Collection("users").Document(FirebaseAuth.CurrentUser.UserId).GetSnapshotAsync().ContinueWith(
                    result =>
                    {
                        var data = result.Result.ToDictionary();
                        var username = data["Username"].ToString();
                        var email = data["EmailAddress"].ToString();
                        CurrentPlayerData = new PlayerData {
                            Username = username, 
                            EmailAddress = email
                        };
                        CurrentPlayerData.GlobalRating = (int) data["GlobalRating"];
                        CurrentPlayerData.GlobalScore = (int) data["GlobalScore"];
                        if (data["Achievements"] != null)
                        {
                            CurrentPlayerData.Achievements = ((string[])data["Achievements"]).ToList();
                        }
                    });
            }
        }
    }
}
