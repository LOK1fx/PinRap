namespace LOK1game
{
    public class PlayerController : Controller
    {
        protected override void Update()
        {
            if (ControlledPawn != null)
                ControlledPawn.OnInput(this);
        }
    }
}