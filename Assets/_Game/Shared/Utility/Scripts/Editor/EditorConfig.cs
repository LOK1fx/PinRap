using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;
using System.IO;

namespace LOK1game.Editor
{
    public enum ELaunchGameOption
    {
        AsClient,
        AsServer,
        AsHost,
    }

    public enum ESpawnType
    {
        Standard,
        FromCameraPostion,
    }

    [InitializeOnLoad()]
    public static class EditorConfig
    {
        private const string PATH = "Configs/Editor";
        private const string FILE_NAME = "EditorLaunchConfig.json";

        private static Config _config;

        static EditorConfig()
        {
            var json = File.ReadAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}");

            _config = JsonConvert.DeserializeObject<Config>(json);
        }

        public static void SetGameLaunchOption(ELaunchGameOption option)
        {
            _config.LaunchGameOption = option;

            Save();
        }

        public static void SetSpawnType(ESpawnType type)
        {
            _config.SpawnType = type;

            Save();
        }

        public static Config GetConfig()
        {
            var json = File.ReadAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}");

            return JsonConvert.DeserializeObject<Config>(json);
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(_config, Formatting.Indented);

            File.WriteAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}", json);
        }

        [System.Serializable]
        public class Config
        {
            public ELaunchGameOption LaunchGameOption = ELaunchGameOption.AsClient;
            public ESpawnType SpawnType = ESpawnType.Standard;
        }
    }
}