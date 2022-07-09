using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Editor
{
    public enum ELaunchGameOption
    {
        AsClient,
        AsServer,
        AsHost,
    }

    public class EditorConfig : MonoBehaviour
    {
        private const string PATH = "Assets/Configs/Editor/";
        private const string FILE_NAME = "EditorLaunchConfig.json";

        public async void SetGameLaunchOption(ELaunchGameOption option)
        {

        }
    }
}