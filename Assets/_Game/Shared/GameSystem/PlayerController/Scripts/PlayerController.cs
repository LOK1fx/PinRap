namespace LOK1game
{
    public class PlayerController : Controller
    {
        public int Points;


        protected override void Awake()
        {
            
        }

        protected override void Update()
        {
            if(ControlledPawn != null)
                ControlledPawn.OnInput(this);
        }
    }
}