using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance;

	public string currentScene;
	public int currentLevel = 0;
	public GameType currentGameType = GameType.FindThePail;

	public enum GameType {
		FindThePail,
		DogFight,
		Loops,
		BombingRun,
		PickUps,
		TimeTrial
	}

	public string[] findThePailLevels = new string[] { "DemoLevel" };

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	void Start() {
		currentScene = SceneManager.GetActiveScene().name;
	}

	public void LoadLevel() {

		switch (currentGameType) {
			case GameType.FindThePail:
				SceneManager.LoadScene(findThePailLevels[currentLevel]);
				break;
		}
	}

	public void Win() {
		SceneManager.LoadScene("Win");
	}

	public void Lose() {
		SceneManager.LoadScene("Loose");
	}

}
