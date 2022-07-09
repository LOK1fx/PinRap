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

        [Header("GameModes")]
        [SerializeField] private GameModeManager _gameModeManager;
        [SerializeField] private EGameModeId _standardGameModeId;
        [SerializeField] private List<BaseGameMode> _gameModes = new List<BaseGameMode>();

        public override void Intialize()
        {
            var config = EditorConfig.GetConfig();

            Debug.Log($"Game started: {config.LaunchGameOption.ToString()}");

            GameStateManager = new GameStateManager();
            _gameModeManager = new GameModeManager();

            foreach (var gamemode in _gameModes)
            {
                _gameModeManager.AddGameMode(gamemode.Id, gamemode);
            }

            _gameModeManager.SetGameMode(_standardGameModeId);

            OnInitialized?.Invoke();
        }
    }
}
