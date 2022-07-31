using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    public class PinRapEnemy : Pawn
    {
        [SerializeField] private CharacterData _character;
        
        private UIArrowSpawner _arrowSpawner;

        private void Start()
        {
            PlayerHud.Instance.DominationBar.SetEnemyCharacter(_character);
        }

        public override void OnPocces(Controller sender)
        {
            base.OnPocces(sender);

            var controller = Controller as PinRapEnemyController;

            _arrowSpawner = controller.ArrowSpawner;
        }

        public override void OnUnpocces()
        {
            base.OnUnpocces();

            _arrowSpawner = null;
        }

        public override void OnInput(object sender)
        {
            if(_arrowSpawner == null) { return; }
            
            TryBeatArrow(_arrowSpawner.DownArrowChecker);
            TryBeatArrow(_arrowSpawner.LeftArrowChecker);
            TryBeatArrow(_arrowSpawner.UpArrowChecker);
            TryBeatArrow(_arrowSpawner.RightArrowChecker);
        }
        
        private void TryBeatArrow(BeatArrowChecker checker)
        {
            if (checker.IsArrowInbound(out var arrow))
            {
                arrow.Beat();
            }
        }
    }
}