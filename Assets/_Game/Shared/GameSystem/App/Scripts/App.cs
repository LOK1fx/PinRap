using System;
using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game
{
    public sealed class App : MonoBehaviour
    {
        public static ProjectContext ProjectContext { get; private set; }

        [SerializeField] private ProjectContext _projectContext = new ProjectContext();

        private const string APP_GAME_OBJECT_NAME = "[App]";

        #region Boot

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load<App>(APP_GAME_OBJECT_NAME));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = APP_GAME_OBJECT_NAME;
            app.InitializeComponents();

            PersistentScene.Load();
            
            DontDestroyOnLoad(app.gameObject);
        }

        #endregion

        public static void Quit(int exitCode = 0)
        {
            EventManager.Clear();
            PersistentScene.Unload();
            
            Application.Quit(exitCode);
        }

        private void InitializeComponents()
        {
            ProjectContext = _projectContext;
            ProjectContext.Initialize();
        }
    }
}