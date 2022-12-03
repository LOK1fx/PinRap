namespace LOK1game.UI
{
    public interface IPlayerHud
    {
        void Bind(PlayerController controller, PinRapPlayer player);
        void Unbind();
    }
}
