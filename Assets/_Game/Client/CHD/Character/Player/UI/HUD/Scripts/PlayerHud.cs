using UnityEngine;

namespace LOK1game.UI
{
    public class PlayerHud : Singleton<PlayerHud>
    {
        public UIArrowSpawner PlayerArrowSpawner => _playerArrowSpawner;
        public UIArrowSpawner EnemyArrowSpawner => _enemyArrowSpawner;
        public DominationBar DominationBar => _dominationBar;

        [SerializeField] private UIArrowSpawner _playerArrowSpawner;
        [SerializeField] private UIArrowSpawner _enemyArrowSpawner;
        [SerializeField] private DominationBar _dominationBar;
    }
}
