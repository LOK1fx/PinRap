using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.DebugTools
{
    public abstract class DebugConsoleCommandBase
    {
        public string Id => _id;
        public string Description => _description;
        public string Format => _format;
        
        private string _id;
        private string _description;
        private string _format;

        public DebugConsoleCommandBase(string id, string description, string format)
        {
            _id = id;
            _description = description;
            _format = format;
        }

        public abstract void Invoke();
    }

    public class DebugConsoleCommand : DebugConsoleCommandBase
    {
        private Action _callback;
        
        public DebugConsoleCommand(string id, string description, string format, Action command) : base(id, description,
            format)
        {
            _callback = command;
        }

        public override void Invoke()
        {
            _callback.Invoke();
        }
    }
}