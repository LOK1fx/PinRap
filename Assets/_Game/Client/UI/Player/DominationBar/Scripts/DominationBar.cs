using UnityEngine;
using UnityEngine.UI;

namespace LOK1game.UI
{
    public class DominationBar : MonoBehaviour
    {
        private const int MAX_POINTS = 100;
        private const int MIN_POINTS = 0;
        
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private Image _enemyAvatar;
        [SerializeField] private Image _bar;
        [SerializeField] private Image _background;

        [Space]
        [SerializeField] private float _avatarsStepSize = 10f;

        private RectTransform _playerAvatarTransform;
        private RectTransform _enemyAvatarTransform;
        private int _currentPoints = 50;

        private void Awake()
        {
            _playerAvatarTransform = _playerAvatar.GetComponent<RectTransform>();
            _enemyAvatarTransform = _enemyAvatar.GetComponent<RectTransform>();
        }

        public void SetPoints(int points)
        {
            var oldPoints = _currentPoints;

            _currentPoints = points;

            ClampPoints();
            UpdateBar(oldPoints - _currentPoints);
        }

        public void AddPoints(int points)
        {
            var oldPoints = _currentPoints;
            
            _currentPoints += points;
            
            ClampPoints();
            UpdateBar(oldPoints - _currentPoints);
        }

        public void RemovePoints(int points)
        {
            var oldPoints = _currentPoints;
            
            _currentPoints -= points;
            
            ClampPoints();
            UpdateBar(oldPoints - _currentPoints);
        }
        
        public void SetPlayerCharacter(CharacterData characterData)
        {
            _playerAvatar.sprite = characterData.MiniAvatar;
        }

        public void SetEnemyCharacter(CharacterData characterData)
        {
            _background.color = characterData.AccentColor;
            _enemyAvatar.sprite = characterData.MiniAvatar;
        }

        private void ClampPoints()
        {
            _currentPoints = Mathf.Clamp(_currentPoints, MIN_POINTS, MAX_POINTS);
        }

        private void UpdateBar(int pointsDifference)
        {
            _bar.transform.localScale = new Vector3(_currentPoints * 0.01f, 1f, 1f);
            
            MoveAvatar(_playerAvatarTransform, pointsDifference);
            MoveAvatar(_enemyAvatarTransform, pointsDifference);
        }

        private void MoveAvatar(RectTransform avatarTransform, int difference)
        {
            avatarTransform.localPosition += Vector3.left * (difference * _avatarsStepSize);
        }
    }
}