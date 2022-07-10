using UnityEngine;

namespace LOK1game.Game
{
    public abstract class GameSession
    {
        public GameSession(bool local, bool host, bool server)
        {
            IsLocal = local;
            IsHost = host;
            IsServer = server;
        }

        public float PlayTime { get; private set; }
        public bool IsLocal { get; }
        public bool IsHost { get; }
        public bool IsServer { get; }

        public abstract void Start();
        public abstract void Update();

        public void UpdateTimer()
        {
            if(IsServer && !IsHost) { return; }

            PlayTime += Time.deltaTime;
        }

        public override string ToString()
        {
            var description = $"Play time: {PlayTime}\n" +
                $"IsLocal: {IsLocal}\n" +
                $"IsHost: {IsHost}\n" +
                $"IsServer: {IsServer}";

            return description;
        }
    }
}