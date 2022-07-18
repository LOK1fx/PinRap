using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SilentlyLoadLevel
{
    public static void LoadScenes(LevelData LevelData)
    {
        for (int i = 0; i < LevelData.ScenesInLevelIndex.Count; i++)
        {
            SceneManager.LoadScene(LevelData.ScenesInLevelIndex[i], LoadSceneMode.Additive);
        }
    }
}
