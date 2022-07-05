namespace LOK1game.Testers
{
    public class BeatControllerTester : BaseTester
    {
        public override void Test1()
        {
            ClientApp.ClientContext.BeatController.InstantiateBeat(EBeatEffectStrength.Weak);
        }

        public override void Test2()
        {
            ClientApp.ClientContext.BeatController.InstantiateBeat(EBeatEffectStrength.Medium);
        }

        public override void Test3()
        {
            ClientApp.ClientContext.BeatController.InstantiateBeat(EBeatEffectStrength.Strong);
        }
    }
}