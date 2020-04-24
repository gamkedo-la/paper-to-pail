﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseToDemoScene : MonoBehaviour {

	void Update() {
		if (Input.GetMouseButton(0)) {
			SceneManager.LoadScene("DemoLevel");
		}
	}
}
