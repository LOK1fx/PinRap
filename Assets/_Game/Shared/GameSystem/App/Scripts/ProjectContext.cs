using LOK1game.Game;
using LOK1game.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [Serializable]
    public sealed class ProjectContext : Context
    {
        public event Action OnInitialized;

        public GameModeManager GameModeManager => _gameModeManager;
        public GameStateManager GameStateManager { get; private set; }
        public GameSession GameSession { get; private set; }

        [Header("GameModes")]
        [SerializeField] private GameModeManager _gameModeManager;
        [SerializeField] private EGameModeId _standardGameModeId;
        [SerializeField] private List<BaseGameMode> _gameModes = new List<BaseGameMode>();

        public override void Intialize()
        {
            GameStateManager = new GameStateManager();
            _gameModeManager = new GameModeManager();

            if(!PlayerConfig.IsInitialized)
                PlayerConfig.Initialize();

            SetupGameSession(PlayerConfig.GetLaunchConfig());

            foreach (var gamemode in _gameModes)
            {
                _gameModeManager.AddGameMode(gamemode.Id, gamemode);
            }

            _gameModeManager.SetGameMode(_standardGameModeId);

            OnInitialized?.Invoke();
        }

        private void SetupGameSession(LaunchConfig config)
        {
            Debug.Log(config.LaunchGameOption);

            bool localGame;
            bool server;
            bool host;

            switch (config.LaunchGameOption)
            {
                case ELaunchGameOption.AsClient:
                    localGame = true;
                    host = false;
                    server = false;
                    break;
                case ELaunchGameOption.AsServer:
                    localGame = false;
                    host = false;
                    server = true;
                    break;
                case ELaunchGameOption.AsHost:
                    localGame = true;
                    host = true;
                    server = false;
                    break;
                default:
                    localGame = true;
                    host = false;
                    server = false;
                    break;
            }

            GameSession = new PinRapGameSession(localGame, server, host);

            Debug.Log(GameSession.ToString());
        }
    }
}
