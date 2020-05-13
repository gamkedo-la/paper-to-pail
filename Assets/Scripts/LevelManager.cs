using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance;

	public string currentScene;
	public int currentLevel = 0;
	public GameType currentGameType = GameType.FindThePail;

	[SerializeField]
	private string[] findThePailLevels = new string[] { "ftpTest1", "ftpTest2", "ftpTest3", "DemoLevel" };

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
			if (currentLevel >= findThePailLevels.Length) {
				currentLevel = 0;
				SceneManager.LoadScene("Title");
			} else {
				SceneManager.LoadScene(findThePailLevels[currentLevel]);
			}
			break;
		}
	}

	public void Win() {
		LevelComplete();
	}

	public void Lose() {
		SceneManager.LoadScene("Lose");
	}

	public void LevelComplete() {
		SceneManager.LoadScene("Win");
	}

	public void NextLevel() {
		currentLevel++;
		LoadLevel();
	}

	public void RestartLevel() {
		LoadLevel();
	}
}

public enum GameType {
	FindThePail,
	DogFight,
	Loops,
	BombingRun,
	PickUps,
	TimeTrial
}