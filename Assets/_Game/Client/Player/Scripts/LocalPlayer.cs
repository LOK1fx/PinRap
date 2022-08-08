using UnityEngine;

namespace LOK1game.PinRap
{
    public static class LocalPlayer
    {
        public static Camera Camera { get; private set; }
        public static Vector3 Position => _player.transform.position;
        public static PinRapPlayerInput Input { get; private set; }
        public static PlayerController Controller { get; private set; }

        private static PinRapPlayer _player;

        public static void Initialize(PinRapPlayer player)
        {
            _player = player;
            
            Input = _player.Input;
            Controller = _player.Controller as PlayerController;
            Camera = Camera.main;
        }

        public static void Kill()
        {
            _player.Kill();
        }
    }
}