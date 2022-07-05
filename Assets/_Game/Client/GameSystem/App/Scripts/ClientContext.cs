using System;
using UnityEngine;
using LOK1game.Game;

namespace LOK1game
{
    [Serializable]
    public class ClientContext : Context
    {
        public event Action OnInitialized;

        public BeatController BeatController { get; private set; }
        public PauseController PauseController { get; private set; }

        [SerializeField] private PauseController _pauseController;

        public override void Intialize()
        {
            BeatController = new BeatController();
            PauseController = PauseController.Spawn(_pauseController);

            OnInitialized?.Invoke();
        }
    }
}