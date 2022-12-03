using UnityEngine;

namespace LOK1game.UI
{
    public class PlayerHud : Singleton<PlayerHud>, IPlayerHud
    {
        public UIArrowSpawner PlayerArrowSpawner => _playerArrowSpawner;
        public UIArrowSpawner EnemyArrowSpawner => _enemyArrowSpawner;

        [SerializeField] private UIArrowSpawner _playerArrowSpawner;
        [SerializeField] private UIArrowSpawner _enemyArrowSpawner;
        [SerializeField] private DominationBar _dominationBar;

        private PlayerController _playerController;
        private PinRapPlayer _player;

        public void Bind(PlayerController controller, PinRapPlayer player)
        {
            _playerController = controller;
            _player = player;

            _player.OnPointsRefreshed += OnPlayerSuccesfullBeat;
            _dominationBar.SetPlayerCharacter(_player.CharacterData);
        }

        public void Unbind()
        {
            _player.OnPointsRefreshed -= OnPlayerSuccesfullBeat;
        }

        public void SetEnemyAvatar(CharacterData data)
        {
            _dominationBar.SetEnemyCharacter(data);
        }

        private void OnPlayerSuccesfullBeat()
        {
            _dominationBar.SetPoints(_playerController.Points);
        }
    }
}
