using System.Collections.Generic;
using Firebase.Firestore;

namespace PlayerData.Manager
{
    [FirestoreData]
    public struct PlayerData
    {
        [FirestoreProperty] public string Username { get; set; }
        [FirestoreProperty] public string EmailAddress { get; set; }
        [FirestoreProperty] public int GlobalScore { get; set; }
        [FirestoreProperty] public int GlobalRating { get; set; }
        [FirestoreProperty] public List<string> Achievements { get; set; }
    }
}