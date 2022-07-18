using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SilentlyLoadLevel
{
    public static void LoadScenes(LevelData LevelData)
    {
        SceneManager.LoadScene(LevelData.MainScene);
        for (int i = 0; i < LevelData.AdditiveScenes.Count; i++)
        {
            SceneManager.LoadScene(LevelData.AdditiveScenes[i], LoadSceneMode.Additive);
        }       
    }
}
