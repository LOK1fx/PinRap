using UnityEngine;

namespace LOK1game.PinRap
{
    public static class LocalPlayer
    {
        public static Vector3 Position => _player.transform.position;
        public static PlayerController Controller { get; private set; }

        private static PinRapPlayer _player;

        public static void Initialize(PinRapPlayer player)
        {
            _player = player;
            
            Controller = player.Controller as PlayerController;
        }

        public static void Kill()
        {
            _player.Kill();
        }
    }
}