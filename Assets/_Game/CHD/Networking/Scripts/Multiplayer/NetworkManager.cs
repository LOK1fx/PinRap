using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;

namespace LOK1game.New.Networking
{
    public enum EServerToClientId : ushort
    {
        SyncTick = 1,
        SyncLevel,
        SyncGamemode,
        PlayerSpawned,
        PlayerRespawn,
        PlayerMovement,
        PlayerHited,
        PlayerDeath,
        PlayerHealth,
        PlayerLand,
        PlayerLoadout,
        PlayerSwitchWeapon,
        Pong,
    }

    public enum EClientToServerId : ushort
    {
        Name = 1,
        Input,
        Ping,
        HitPlayer,
        SwitchWeapon,
    }

    public class NetworkManager : Singleton<NetworkManager>
    {
        public Client Client { get; private set; }
        public ushort ServerTick
        {
            get => _serverTick;

            private set
            {
                _serverTick = value;
                InterpolationTick = (ushort)(value - TicksBetweenPositionUpdates);
            }
        }

        private ushort _serverTick;

        public ushort InterpolationTick { get; private set; }

        public ushort TicksBetweenPositionUpdates
        {
            get => _ticksBetweenPositionUpdates;

            private set
            {
                _ticksBetweenPositionUpdates = value;
                InterpolationTick = (ushort)(ServerTick - value);
            }
        }

        private ushort _ticksBetweenPositionUpdates;

        [SerializeField] private string _ip = Constants.Network.LOCAL_HOST_IP;
        [SerializeField] private ushort _port = Constants.Network.SERVER_DEFAULT_PORT;

        [Space(10)]
        [SerializeField] private ushort _tickDivergenceTolerance = 1;

        protected override void Awake()
        {
            base.Awake();

            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

            Client = new Client();
        }

        private void Start()
        {
            Client.Connected += DidConnect;
            Client.ConnectionFailed += FailedToConnect;
            Client.ClientDisconnected += PlayerLeft;
            Client.Disconnected += DidDisconnect;

            ServerTick = 2;
        }

        private void FixedUpdate()
        {
            Client.Tick();
            ServerTick++;
        }

        public void Connect()
        {
            Client.Connect($"{_ip}:{_port}");
        }

        public void SetIp(string ip)
        {
            _ip = ip;
        }

        private void DidConnect(object sender, EventArgs e)
        {
            NetworkUIManager.Instance.SendName();
        }

        private void FailedToConnect(object sender, EventArgs e)
        {
            NetworkUIManager.Instance.BackToMain();
        }

        private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
        {
            if(NetworkPlayer.List.TryGetValue(e.Id, out var player))
            {
                Destroy(player.gameObject);
            }
        }

        private void DidDisconnect(object sender, EventArgs e)
        {
            NetworkUIManager.Instance.BackToMain();

            foreach (var player in NetworkPlayer.List.Values)
            {
                Destroy(player.gameObject);
            }
        }

        private void SetTick(ushort serverTick)
        {
            if (Mathf.Abs(ServerTick - serverTick) > _tickDivergenceTolerance)
            {
                Debug.Log($"Client tick: {ServerTick} -> {serverTick}");
                ServerTick = serverTick;
            }
        }

        #region Messages

        [MessageHandler((ushort)EServerToClientId.SyncTick)]
        private static void SyncTick(Message message)
        {
            Instance.SetTick(message.GetUShort());
        }

        [MessageHandler((ushort)EServerToClientId.SyncLevel)]
        private static void SyncLevel(Message message)
        {
            NetworkGameLogic.Instance.SetLevel(message.GetUShort());
        }

        [MessageHandler((ushort)EServerToClientId.SyncGamemode)]
        private static void SyncGameMode(Message message)
        {
            var gamemode = (EGameModeId)message.GetUShort();

            App.ProjectContext.GameModeManager.SetGameMode(gamemode);
        }

        #endregion

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

            Client.Disconnect();
        }
    }

}