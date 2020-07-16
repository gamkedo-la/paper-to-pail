using System.Collections;
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
            case SceneType.Win:
				LevelManager.Instance.NextLevel();
			    break;
            case SceneType.Lose:
			    LevelManager.Instance.RestartLevel();
			    break;
			case SceneType.IntroOrCredits:
				LevelManager.Instance.SetTitle();
				break;
		}
    }
}

public enum SceneType {
    Win,
    Lose,
	IntroOrCredits
}