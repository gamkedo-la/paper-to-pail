using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance;

	public string currentScene;
	public int currentLevel = 0;
	public GameType currentGameType = GameType.FindThePail;

	private int goals = 0;
	
	private string[][] levels = { new[] { "ftpTest2", "ftpTest4", "ftpTest3", "ftpTest1", "blackout", "Down", "factory" }, //Find the Pail
								  new[] { "dogfightFactoryArena" } }; //Dog Fight
	
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

	void Update() {
		if (Input.GetButton("NextLevel")) {
			currentLevel++;
			LoadLevel();
		}

		if (Input.GetButton("LastLevel")) {
			currentLevel--;
			LoadLevel();
		}
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
		SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
		currentScene = "Lose";
	}

	public void Title() {
		SceneManager.LoadScene("Title");
		currentScene = "Title";
	}

	public void Credits() {
		return;
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
		
		// Remove persisted player objects.
		var planes = GameObject.FindGameObjectsWithTag("Player");
		foreach (var plane in planes)
		{
			Destroy(plane);
		}
		
		currentScene = "Win";
		SceneManager.LoadScene("Win", LoadSceneMode.Additive);
	}

	public void NextLevel() {
		currentLevel++;
		LoadLevel();
		MusicManager.Instance.nextTrack();
	}

	public void RestartLevel() {
		LoadLevel();
	}

	public void AddGoal() {
		goals++;
	}

	public void RemoveGoal() {
		goals--;
		if (goals <= 0) {
			Win();
		}
	}
}

public enum GameType {
	FindThePail,
	DogFight
}