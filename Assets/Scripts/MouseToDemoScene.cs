﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseToDemoScene : MonoBehaviour {
    public SceneType sceneType;

    void Update() {

        if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) {
            ProgressToNextScene();
        }
    }

    private void ProgressToNextScene() {
        switch (sceneType) {
            case SceneType.Title:
            LevelManager.Instance.LoadLevel();
            break;
            case SceneType.Win:
            LevelManager.Instance.NextLevel();
            break;
            case SceneType.Lose:
            LevelManager.Instance.RestartLevel();
            break;
        }
    }
}

public enum SceneType {
    Title,
    Win,
    Lose
}