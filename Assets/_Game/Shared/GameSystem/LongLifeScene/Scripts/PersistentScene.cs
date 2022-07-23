using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    [Obsolete] //Was needed for a feature, but later just get too buggy
    public static class PersistentScene
    {
        public static bool IsLoaded { get; private set; }
        
        private const string SCENE_NAME = "Persistent";

        private static Scene _loadedScene;

        public static void AddObject(GameObject gameObject)
        {
            if(IsLoaded == false) { return; }
            
            var newGameObject = GameObject.Instantiate(gameObject);
            
            SceneManager.MoveGameObjectToScene(newGameObject, _loadedScene);
        }
        
        public static void Load()
        {
            if(IsLoaded) { return; }
            
            SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Additive);
            _loadedScene = SceneManager.GetSceneByName(SCENE_NAME);
            
            IsLoaded = true;
        }

        public static void Unload()
        {
            if(IsLoaded == false) { return; }

            SceneManager.UnloadSceneAsync(SCENE_NAME);

            IsLoaded = false;
        }
    }
}