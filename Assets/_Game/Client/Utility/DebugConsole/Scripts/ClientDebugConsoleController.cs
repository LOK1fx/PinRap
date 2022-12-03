using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game;
using LOK1game.UI;

namespace LOK1game.DebugTools
{
    public class ClientDebugConsoleController : MonoBehaviour
    {
        private List<DebugConsoleCommandBase> _commands;

        #region Commands

        private static DebugConsoleCommand _stopMusicTimelinePlaybackCommand;
        
        private static DebugConsoleCommand _instantiateBeatEffectCommand;
        private static DebugConsoleCommand _instantiateBeatCommand;
        
        private static DebugConsoleCommand _spawnPlayerBeatArrowLeft;
        private static DebugConsoleCommand _spawnPlayerBeatArrowDown;
        private static DebugConsoleCommand _spawnPlayerBeatArrowUp;
        private static DebugConsoleCommand _spawnPlayerBeatArrowRight;
        
        private static DebugConsoleCommand _spawnEnemyBeatArrowLeft;
        private static DebugConsoleCommand _spawnEnemyBeatArrowDown;
        private static DebugConsoleCommand _spawnEnemyBeatArrowUp;
        private static DebugConsoleCommand _spawnEnemyBeatArrowRight;

        private static DebugConsoleCommand _addPointsToBar;
        private static DebugConsoleCommand _removePointsFromBar;

        private static DebugConsoleCommand _spawnPopupText;

        #endregion
        
        private bool _showConsole;
        private string _input = "";

        private void Awake()
        {
            InitializeCommands();
            UpdateCommandsList();
        }

        private void UpdateCommandsList()
        {
            _commands = new List<DebugConsoleCommandBase>()
            {
                _stopMusicTimelinePlaybackCommand,
                _instantiateBeatEffectCommand,
                _instantiateBeatCommand,
                _spawnPlayerBeatArrowLeft, //--Player arrows
                _spawnPlayerBeatArrowDown,
                _spawnPlayerBeatArrowUp,
                _spawnPlayerBeatArrowRight,//--
                _spawnEnemyBeatArrowLeft, //--Enemy arrows
                _spawnEnemyBeatArrowDown,
                _spawnEnemyBeatArrowUp,
                _spawnEnemyBeatArrowRight, //--
                _addPointsToBar,
                _removePointsFromBar,
                _spawnPopupText,
            };
        }

        private void InitializeCommands()
        {
            _stopMusicTimelinePlaybackCommand = new DebugConsoleCommand("cl_music_timeline_stop", "",
                "cl_music_timeline_stop",
                () =>
                {
                    if(MusicTimeline.Instance != null)
                        MusicTimeline.Instance.StopPlayback();
                });
            _instantiateBeatEffectCommand = new DebugConsoleCommand("cl_beat_controller_beat_effect", "",
                "cl_beat_controller_beat_effect",
                () =>
                {
                    if(BeatEffectController.Instance != null)
                        BeatEffectController.Instance.InstantiateBeat(EBeatEffectStrength.Medium);
                });
            _instantiateBeatCommand = new DebugConsoleCommand("cl_beat_controller_beat", "",
                "cl_beat_controller_beat",
                () =>
                {
                    ClientApp.ClientContext.BeatController.InstantiateBeat(EBeatEffectStrength.Medium);
                });
            _spawnPopupText = new DebugConsoleCommand("cl_spawn_popup", "",
                "cl_spawn_popup",
                () =>
                {
                    var textParam = new PopupTextParams("200", 5f, Color.white);

                    PopupText.Spawn<PopupText3D>(Vector3.zero, textParam);
                });
        }

        private void Update()
        {
#if LOK1GAME_STANDALONE
    #if UNITY_EDITOR
            if(Input.GetButtonDown("ToggleDebugConsole"))
                ToggleConsole();
            if(Input.GetButtonDown("Submit"))
                Submit();
    #endif
#endif
        }

        private void ToggleConsole()
        {
            _showConsole = !_showConsole;
        }

        private void Submit()
        {
            if(_showConsole == false) { return; }
            
            HandleInput();
            _input = "";
        }

        private void HandleInput()
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                var command = _commands[i];

                if (_input.Contains(command.Id))
                {
                    command.Invoke();
                }
            }
        }

        private void OnGUI()
        {
            if(_showConsole == false) { return; }

            var y = 0f;
            
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);

            _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
        }
    }
}