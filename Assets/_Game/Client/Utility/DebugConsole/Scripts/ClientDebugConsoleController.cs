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

        private static DebugConsoleCommand _startMusicTimelinePlaybackCommand;
        private static DebugConsoleCommand _stopMusicTimelinePlaybackCommand;
        private static DebugConsoleCommand _instantiateBeatEffectCommand;
        private static DebugConsoleCommand _instantiateBeatCommand;
        private static DebugConsoleCommand _spawnBeatArrowLeft;
        private static DebugConsoleCommand _spawnBeatArrowDown;
        private static DebugConsoleCommand _spawnBeatArrowUp;
        private static DebugConsoleCommand _spawnBeatArrowRight;
        
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
                _startMusicTimelinePlaybackCommand,
                _stopMusicTimelinePlaybackCommand,
                _instantiateBeatEffectCommand,
                _instantiateBeatCommand,
                _spawnBeatArrowLeft,
                _spawnBeatArrowDown,
                _spawnBeatArrowUp,
                _spawnBeatArrowRight,
            };
        }

        private void InitializeCommands()
        {
            _startMusicTimelinePlaybackCommand = new DebugConsoleCommand("cl_music_timeline_start", "",
                "cl_music_timeline_start",
                () =>
                {
                    if(MusicTimeline.Instance != null)
                        MusicTimeline.Instance.StartPlayback(ClientApp.ClientContext.MusicDatabase.MusicDatas[0]);
                });
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
            _spawnBeatArrowLeft = new DebugConsoleCommand("cl_spawn_arrow_left", "",
                "cl_spawn_arrow_left",
                () =>
                {
                    if(PlayerHud.Instance != null)
                        PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Left);
                });
            _spawnBeatArrowDown = new DebugConsoleCommand("cl_spawn_arrow_down", "",
                "cl_spawn_arrow_down",
                () =>
                {
                    if(PlayerHud.Instance != null)
                        PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Down);
                });
            _spawnBeatArrowUp = new DebugConsoleCommand("cl_spawn_arrow_up", "",
                "cl_spawn_arrow_up",
                () =>
                {
                    if(PlayerHud.Instance != null)
                        PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Up);
                });
            _spawnBeatArrowRight = new DebugConsoleCommand("cl_spawn_arrow_right", "",
                "cl_spawn_arrow_right",
                () =>
                {
                    if(PlayerHud.Instance != null)
                        PlayerHud.Instance.PlayerArrowSpawner.Spawn(EArrowType.Right);
                });
        }

        private void Update()
        {
            if(Input.GetButtonDown("ToggleDebugConsole"))
                ToggleConsole();
            if(Input.GetButtonDown("Submit"))
                Submit();
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