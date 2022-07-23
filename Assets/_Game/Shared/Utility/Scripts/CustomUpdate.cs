using System;
using UnityEngine;

namespace LOK1game.Tools
{
    public class CustomUpdate : MonoBehaviour
    {
        private static CustomUpdate _instance;
        
        private Action _updateCallback;
        private Action _fixedUpdateCallback;
        
        private void Update()
        {
            _updateCallback?.Invoke();
        }

        private void FixedUpdate()
        {
            _fixedUpdateCallback?.Invoke();
        }
        
        
        public static void AddUpdateCallback(Action callback)
        {
            _instance._updateCallback += callback;
        }

        public static void RemoveUpdateCallback(Action callback)
        {
            _instance._updateCallback -= callback;
        }

        public static void AddFixedUpdateCallback(Action callback)
        {
            _instance._fixedUpdateCallback += callback;
        }

        public static void RemoveFixedUpdateCallback(Action callback)
        {
            _instance._fixedUpdateCallback -= callback;
        }
        

        private static void CheckForInstance()
        {
            if (_instance == null) {
                _instance = new GameObject("[CustomUpdate]").AddComponent<CustomUpdate>();
            }
        }
    }
}