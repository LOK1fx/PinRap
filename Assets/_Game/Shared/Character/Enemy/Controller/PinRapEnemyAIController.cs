
namespace LOK1game
{
    public class PinRapEnemyAIController : Controller
    {
        protected override void Update()
        {
            if(ControlledPawn == null) { return; }
            
            ControlledPawn.OnInput(this);
        }
    }
}