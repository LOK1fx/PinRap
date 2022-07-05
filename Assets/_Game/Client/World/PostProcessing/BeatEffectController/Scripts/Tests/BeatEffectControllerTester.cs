namespace LOK1game.Testers
{
    public class BeatEffectControllerTester : BaseTester
    {
        public override void Test1()
        {
            BeatEffectController.Instance.InstantiateBeat(EBeatEffectStrength.Weak);
        }

        public override void Test2()
        {
            BeatEffectController.Instance.InstantiateBeat(EBeatEffectStrength.Medium);
        }

        public override void Test3()
        {
            BeatEffectController.Instance.InstantiateBeat(EBeatEffectStrength.Strong);
        }
    }
}