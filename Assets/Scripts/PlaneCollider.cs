using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneCollider : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Trashcan") {
			SceneManager.LoadScene("Win");
		} else if (other.gameObject.tag != "Player") {
			SceneManager.LoadScene("Loose");
		}
	}
}
