﻿using System;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace LOK1game.Game
{
    public static class PlayerConfig
    {
        private const string PATH = "Configs/Player";
        private const string LAUNCH_CONFIG_NAME = "PlayerLaunchConfig.json";

        public static bool IsIntialized { get; private set; }

        private static LaunchConfig _launchConfig;

        public static void Intialize()
        {
            if (IsIntialized)
                throw new Exception("PlayerConfig is already initialized!");

            GenerateLaunchConfig();

            _launchConfig = LoadJson<LaunchConfig>();
        }

        public static LaunchConfig GetLaunchConfig()
        {
            return _launchConfig;
        }

        public static void SaveLaunchConfig()
        {
            var json = JsonConvert.SerializeObject(_launchConfig, Formatting.Indented);

            File.WriteAllText($"{Application.dataPath}/{PATH}/{LAUNCH_CONFIG_NAME}", json);
        }

        private static void GenerateLaunchConfig()
        {
            var path = $"{Application.dataPath}/{PATH}";
            var fullPath = $"{path}/{LAUNCH_CONFIG_NAME}";

            if(!File.Exists(fullPath))
            {
                Directory.CreateDirectory(path);

                var file = File.Create(fullPath);
                file.Dispose();
            } 

            var defaultConfig = new LaunchConfig();
            var json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);

            File.WriteAllText(fullPath, json);
        }

        private static T LoadJson<T>()
        {
            var path = $"{Application.dataPath}/{PATH}";
            var fullPath = $"{path}/{LAUNCH_CONFIG_NAME}";
            string json;

            if (File.Exists(fullPath))
            {
                json = File.ReadAllText(fullPath);
            }  
            else
            {
                GenerateLaunchConfig();

                return LoadJson<T>();
            }

            Debug.Log(fullPath);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
