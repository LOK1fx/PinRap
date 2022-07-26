using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public Sprite MiniAvatar => _miniAvatar;
        public string Name => _name;
        
        [SerializeField] private Sprite _miniAvatar;
        [SerializeField] private string _name;
    }
}