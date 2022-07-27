using System;
using System.Linq;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

namespace _Game.Client.Firebase
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseApp FirebaseApp;
        public static FirebaseFirestore FirebaseFirestore;
        public static FirebaseAuth FirebaseAuth;
        public static PlayerData.Manager.PlayerData CurrentPlayerData;
        public static bool Authorized;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (task.Result == DependencyStatus.Available)
                {
                    InitializationFirebase();
                } 
                else 
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        private static void InitializationFirebase()
        {
            FirebaseApp = FirebaseApp.DefaultInstance;
            FirebaseFirestore = FirebaseFirestore.DefaultInstance;
            FirebaseAuth = FirebaseAuth.DefaultInstance;
            Authorized = FirebaseAuth.CurrentUser != null;
            if (FirebaseAuth.CurrentUser == null) return;
            SetUserByUid(FirebaseAuth.CurrentUser.UserId);
        }
        
        public static void CreateUser(string name, string email, string password, bool isCurrentPlayer)
        {
            FirebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(result =>
            {
                var playerData = new PlayerData.Manager.PlayerData {Username = name, EmailAddress = email};
                if (isCurrentPlayer)
                {
                    CurrentPlayerData = playerData;
                }
                FirebaseFirestore.Collection("users").Document(result.Result.UserId)
                    .SetAsync(playerData);
            });
            Authorized = isCurrentPlayer;
        }
        
        public static void AuthUser(string email, string password, Action<bool> onResult)
        {
            FirebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
                result =>
                {
                    if (FirebaseAuth.CurrentUser == null)
                    {
                        Authorized = false;
                        onResult.Invoke(false);
                        return;
                    }
                    Authorized = true;
                    SetUserByUid(FirebaseAuth.CurrentUser.UserId);
                    onResult.Invoke(true);
                });
        }

        public static void SignOut()
        {
            FirebaseAuth.SignOut();
            CurrentPlayerData = new PlayerData.Manager.PlayerData{Username = null};
            Authorized = false;
        }

        public static void SetUserByUid(string uid)
        {
            FirebaseFirestore.Collection("users").Document(uid).GetSnapshotAsync().ContinueWith(
                        result =>
                        {
                            var data = result.Result.ToDictionary();
                            var username = data["Username"].ToString();
                            var email = data["EmailAddress"].ToString();
                            CurrentPlayerData = new PlayerData.Manager.PlayerData
                            {
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