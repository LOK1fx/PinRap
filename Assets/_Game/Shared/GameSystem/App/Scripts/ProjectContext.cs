using LOK1game.Game;
using System;
using System.Collections.Generic;
using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game
{
    [Serializable]
    public sealed class ProjectContext : Context
    {
        public GameModeManager GameModeManager => _gameModeManager;
        public GameStateManager GameStateManager { get; private set; }
        public GameSession GameSession { get; private set; }

        public ExperienceManager ExperienceManager { get; private set; }

        [Header("GameModes")]
        [SerializeField] private GameModeManager _gameModeManager;
        [SerializeField] private EGameModeId _standardGameModeId;
        [SerializeField] private List<BaseGameMode> _gameModes = new List<BaseGameMode>();

        public override void Initialize()
        {
            GameStateManager = new GameStateManager();
            ExperienceManager = new ExperienceManager();
            _gameModeManager = new GameModeManager();

            if(!PlayerConfig.IsInitialized)
                PlayerConfig.Initialize();

            SetupGameSession(PlayerConfig.GetLaunchConfig());

            foreach (var gameMode in _gameModes)
            {
                _gameModeManager.AddGameMode(gameMode.Id, gameMode);
            }

            _gameModeManager.SetGameMode(_standardGameModeId);

            var evt = new OnProjectContextInitializedEvent(this);
            
            EventManager.Broadcast(evt);

            //evt.Dispose();
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
