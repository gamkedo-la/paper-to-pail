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

	private int goals = 0;
	
	private string[][] levels = { new[] { "ftpTest1", "ftpTest2", "ftpTest3" }, //Find the Pail
								  new[] { "dogfightTest" }, //Dog Fight
								  new[] { "loopTest1", "loopTest2" }, //Loops
								  new[] { "DemoLevel" }, //Bombing Run
								  new[] { "DemoLevel" }, //Pickups
								  new[] { "DemoLevel" } };//Time Trials

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		currentScene = SceneManager.GetActiveScene().name;
	}

	public void LoadLevel() {
		
		if (currentLevel >= levels[(int)currentGameType].Length) {
			currentLevel = 0;
			SceneManager.LoadScene("Title");
			currentScene = "Title";
		} else {
			currentScene = levels[(int)currentGameType][currentLevel];
			SceneManager.LoadScene(levels[(int)currentGameType][currentLevel]);
		}

		goals = 0;
	}

	public void Win() {
		LevelComplete();
	}

	public void Lose() {
		SceneManager.LoadScene("Lose");
		currentScene = "Lose";
	}

	public void Title() {
		SceneManager.LoadScene("Title");
		currentScene = "Title";
	}

	public void LoadTitle() {
		SceneManager.LoadScene("Title", LoadSceneMode.Additive);
	}

	public void SetTitle() {
		if (!SceneManager.GetSceneByName("Title").isLoaded) {
			Title();
			return; 
		}
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Title"));
		SceneManager.UnloadSceneAsync(currentScene);
		currentScene = "Title";
	}

	public void LevelComplete() {
		currentScene = "Win";
		SceneManager.LoadScene("Win");
	}

	public void NextLevel() {
		currentLevel++;
		LoadLevel();
	}

	public void RestartLevel() {
		LoadLevel();
	}

	public void AddGoal() {
		goals++;
		Debug.Log(goals);
	}

	public void RemoveGoal() {
		goals--;
		Debug.Log(goals);
		if (goals <= 0) {
			Win();
		}
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