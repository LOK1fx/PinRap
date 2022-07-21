using UnityEngine;
using System;

namespace LOK1game
{
    public sealed class ClientApp : MonoBehaviour
    {
        public static ClientContext ClientContext { get; private set; }

        [SerializeField] private ClientContext _clientContext = new ClientContext();

        private const string _appGameObjectName = "[ClientApp]";

        #region Boot

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load<ClientApp>(_appGameObjectName));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = _appGameObjectName;
            app.InitializeComponents();

            DontDestroyOnLoad(app);
        }

        #endregion

        private void InitializeComponents()
        {
            ClientContext = _clientContext;
            ClientContext.Initialize();
        }
    }
}