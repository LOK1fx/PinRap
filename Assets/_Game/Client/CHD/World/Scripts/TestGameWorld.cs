using UnityEngine;

namespace LOK1game.World
{
    public class TestGameWorld : GameWorld
    {
        [SerializeField] private EGameModeId _id;

        public override void Intialize()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && Input.GetKeyDown(KeyCode.C))
                App.ProjectContext.GameModeManager.SetGameMode(_id);
        }
    }
}