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
	
	private string[][] levels = { new[] { "ftpTest1", "ftpTest2", "ftpTest3" }, //Find the Pail
								  new[] { "DemoLevel" }, //Dog Fight
								  new[] { "DemoLevel" }, //Loops
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
			currentScene = "Title";
			SceneManager.LoadScene("Title");
		} else {
			currentScene = levels[(int)currentGameType][currentLevel];
			SceneManager.LoadScene(levels[(int)currentGameType][currentLevel]);
		}
	}

	public void Win() {
		LevelComplete();
	}

	public void Lose() {
		currentScene = "Lose";
		SceneManager.LoadScene("Lose");
	}

	public void Title() {
		currentScene = "Title";
		SceneManager.LoadScene("Title");
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
}

public enum GameType {
	FindThePail,
	DogFight,
	Loops,
	BombingRun,
	PickUps,
	TimeTrial
}