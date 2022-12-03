using LOK1game.PinRap.World;
using UnityEngine;

namespace LOK1game.PinRap
{
    public static class LocalPlayer
    {
        public static Camera Camera { get; private set; }
        public static PinRapPlayerInput Input { get; private set; }
        public static PlayerController Controller { get; private set; }

        private static PinRapPlayer _player;
        private static PinRapGameplayWorld _world;

        public static void Initialize(PinRapPlayer player, Controller controller, PinRapGameplayWorld world)
        {
            _player = player;
            _world = world;

            Controller = controller as PlayerController;
            Controller.Points = _world.LevelConfigData.StartPlayerPoints;

            Input = _player.Input;
            Camera = Camera.main;
        }

        public static void AddPoints()
        {
            Controller.Points += _world.LevelConfigData.EarnPoints;

            RefreshPoints();
        }

        public static void RemovePoints()
        {
            Controller.Points -= _world.LevelConfigData.LosedPoints;

            RefreshPoints();
        }

        private static void RefreshPoints()
        {
            _player.PointsChanged();
        }
    }
}