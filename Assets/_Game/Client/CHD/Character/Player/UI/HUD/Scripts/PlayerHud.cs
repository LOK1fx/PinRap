using UnityEngine;

namespace LOK1game.UI
{
    public class PlayerHud : Singleton<PlayerHud>
    {
        public UIArrowSpawner PlayerArrowSpawner => _playerArrowSpawner;

        [SerializeField] private UIArrowSpawner _playerArrowSpawner;
    }
}
