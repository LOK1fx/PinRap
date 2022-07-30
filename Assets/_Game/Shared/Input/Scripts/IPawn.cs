namespace LOK1game
{
    public interface IPawn
    {
        Controller Controller { get; }
        void OnInput(object sender);
        void OnPocces(Controller sender);
        void OnUnpocces();
    }
}