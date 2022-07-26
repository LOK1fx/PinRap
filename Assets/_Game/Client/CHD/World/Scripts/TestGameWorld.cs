using UnityEngine;

namespace LOK1game.World
{
    public class TestGameWorld : GameWorld
    {
        [SerializeField] private EGameModeId _id;

        protected override void Initialize()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && Input.GetKeyDown(KeyCode.C))
                App.ProjectContext.GameModeManager.SetGameMode(_id);
        }
    }
}