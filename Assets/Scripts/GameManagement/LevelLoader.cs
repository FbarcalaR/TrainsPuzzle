﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader: MonoBehaviour {
    public static Action beginSceneReload;

    public static void LoadScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public static void ReloadScene() {
        beginSceneReload?.Invoke();
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.buildIndex);
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
