using System;
using UnityEngine;

namespace LOK1game
{
    [Serializable]
    public class CameraChangeEvent
    {
        public ECharacterCameraFocus Focus;
        public float StartSecond;
        [HideInInspector] public bool IsPlayed;
        
        public enum ECharacterCameraFocus
        {
            Main,
            Center,
            Left,
            Right,
        }
    }
}