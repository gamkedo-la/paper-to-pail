using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public string currentScene;
	public int currentLevel = 0;

	[SerializeField]
	string[] findThePailLevels = new string[] { "DemoLevel" };

	void Start() {
		currentScene = SceneManager.GetActiveScene().name;
	}

	void Update() {

	}
}
