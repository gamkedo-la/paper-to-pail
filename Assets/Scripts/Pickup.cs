using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	void Start() {
		LevelManager.Instance.AddGoal();
	}

	public void Complete() {
		LevelManager.Instance.RemoveGoal();
		Destroy(gameObject);
	}
}
