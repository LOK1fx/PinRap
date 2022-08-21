using UnityEngine;
using System;

namespace LOK1game.SDK
{
    public class SDK : MonoBehaviour
    {
        private const string SDK_GAME_OBJECT_NAME = "[LOK1gameSDK]";


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load<SDK>(SDK_GAME_OBJECT_NAME));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = SDK_GAME_OBJECT_NAME;

            DontDestroyOnLoad(app.gameObject);
        }
    }
}