using UnityEngine;

namespace LOK1game.SDK
{
    [CreateAssetMenu(fileName = "new DataLayer", menuName = "LOK1gameSDK/DataLayer")]
    public class DataLayer : ScriptableObject
    {
        public string Id => _id;

        [SerializeField] private string _id;
    }
}