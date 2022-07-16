using System;
using System.Collections;
using LOK1game.Tools;
using UnityEngine;

namespace LOK1game
{
    public sealed class App : MonoBehaviour
    {
        public static ProjectContext ProjectContext { get; private set; }

        [SerializeField] private ProjectContext _projectContext = new ProjectContext();

        private const string _appGameObjectName = "[App]";

        #region Boot

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load<App>(_appGameObjectName));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = _appGameObjectName;
            app.InitializeComponents();

            PersistentScene.Load();
            
            DontDestroyOnLoad(app.gameObject);
        }

        #endregion

        public static void Quit(int exitCode = 0)
        {
            PersistentScene.Unload();
            Application.Quit(exitCode);
        }

        private void InitializeComponents()
        {
            ProjectContext = _projectContext;
            ProjectContext.Intialize();
        }
    }
}