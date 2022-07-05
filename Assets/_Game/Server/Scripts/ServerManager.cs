using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;

namespace LOK1game.Networking
{
    public class ServerManager : PersistentSingleton<ServerManager>
    {
        private Server _server;

        protected override void Awake()
        {
            base.Awake();

            _server = new Server();
            _server.Start(Constants.Network.SERVER_DEFAULT_PORT, 3);

            RiptideLogger.Initialize(Debug.Log, true);
        }
    }
}