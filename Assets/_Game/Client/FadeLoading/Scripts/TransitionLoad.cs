using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    [RequireComponent(typeof(Animator))]
    public class TransitionLoad : MonoBehaviour
    {
        public static bool IsLoading { get; private set; } = false;

        private const string TRIGGER_CLOSE = "SceneClose";
        private const string TRIGGER_OPEN = "SceneOpen";
        
        private static TransitionLoad _instance;

        private Animator _animator;
        private readonly List<AsyncOperation> _loadingOperations = new List<AsyncOperation>();

        private void Start()
        {
            _instance = this;

            _animator = GetComponent<Animator>();
        }

        public static void LoadLevel(LevelData levelData, string currentSceneName)
        {
            if(IsLoading) { return; }

            SceneManager.UnloadSceneAsync(currentSceneName);
            
            LoadLevel(levelData);
        }

        public static void LoadLevel(LevelData level)
        {
            if(IsLoading) { return; }

            _instance._loadingOperations.Clear();

            IsLoading = true;
            _instance._animator.SetTrigger(TRIGGER_CLOSE);
            _instance._loadingOperations.Add(SceneManager.LoadSceneAsync(level.BuildIndex, LoadSceneMode.Single));

            foreach (var additionalLevel in level.AdditiveScenes)
            {
                _instance._loadingOperations.Add(SceneManager.LoadSceneAsync(additionalLevel, LoadSceneMode.Additive));
            }
            
            SetAllowSceneActivation(false);
        }

        public static void LoadScene(string currentSceneName, string sceneToLoadName)
        {
            if(IsLoading) { return; }

            SceneManager.UnloadSceneAsync(currentSceneName);
            
            IsLoading = true;
            _instance._animator.SetTrigger(TRIGGER_CLOSE);
            _instance._loadingOperations.Add(SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive));
            
            SetAllowSceneActivation(false);
        }

        public void OnAnimationOver()
        {
            _instance._animator.SetTrigger(TRIGGER_OPEN);
            IsLoading = false;
            
            SetAllowSceneActivation(true);
        }

        private static void SetAllowSceneActivation(bool state)
        {
            foreach (var operation in _instance._loadingOperations)
            {
                operation.allowSceneActivation = state;
            }
        }
    }
}