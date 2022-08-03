using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public string Name => _name;
        public Sprite MiniAvatar => _miniAvatar;
        public bool CanAppearInDialogues => _canAppearInDialogues;
        public Sprite DialogueCharacterSprite => dialogueCharacterSprite;

        [Header("BaseInfo")]
        [SerializeField] private string _name;
        [SerializeField] private Sprite _miniAvatar;
        [Header("Story")]
        [SerializeField] private bool _canAppearInDialogues;
        [SerializeField] private Sprite dialogueCharacterSprite;
    }
}