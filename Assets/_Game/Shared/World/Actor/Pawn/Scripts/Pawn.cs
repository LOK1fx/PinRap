namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public bool IsLocal { get; private set; }

        public void SetLocal(bool local)
        {
            IsLocal = local;
        }

        public abstract void OnInput(object sender);
        public abstract void OnPocces(Controller sender);
    }
}