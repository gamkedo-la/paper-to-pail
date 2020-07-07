﻿using System.Collections;
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
	
	private string[][] levels = { new[] { "ftpTest1", "ftpTest2", "ftpTest3", "ftpTest4", "blackout" }, //Find the Pail
								  new[] { "dogfightTest" }, //Dog Fight
								  new[] { "loopTest1", "loopTest2" }, //Loops
								  new[] { "DemoLevel" }, //Bombing Run
								  new[] { "pickupsTest1",  "pickupsTest2"}, //Pickups
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

		// Persist the plane through subsequent plays.
		var planes = GameObject.FindGameObjectsWithTag("Player");
		if (planes.Length > 0) {
			foreach (var plane in planes)
			{
				var flightController = plane.GetComponent<FlightController>();
				if (flightController) {
					Destroy(flightController);
				}
				plane.GetComponent<Rigidbody>().isKinematic = true;
				DontDestroyOnLoad(plane);
			}
		}
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
		// Remove persisted player objects.
		var planes = GameObject.FindGameObjectsWithTag("Player");
		foreach (var plane in planes)
		{
			Destroy(plane);
		}
		
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