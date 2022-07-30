using System;
using LOK1game.UI;

namespace LOK1game
{
    public class PinRapEnemyController : Controller
    {
        public UIArrowSpawner ArrowSpawner { get; private set; }

        protected override void Awake()
        {
            if (PlayerHud.Instance == null)
                throw new Exception("There are no PlayerHud!");

            ArrowSpawner = PlayerHud.Instance.EnemyArrowSpawner;
        }

        protected override void Update()
        {
            ControlledPawn.OnInput(this);
        }
    }
}