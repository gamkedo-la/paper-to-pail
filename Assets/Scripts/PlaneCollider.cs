using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlaneCollider : MonoBehaviour {

	public UnityEvent OnWin = null;
	public UnityEvent OnLose = null;

	public float holdOnWin = 3.0f;
	public float holdOnLose = 1.5f;

	private bool fall = false;

	private void OnTriggerEnter(Collider other) {
		if (fall) {
			return;
		}

		if (other.gameObject.tag == "WinZone") {
			OnWin?.Invoke();
			Invoke(nameof(Win), holdOnWin);
			fall = true;
		} else if (other.gameObject.tag == "Boost") {
			gameObject.GetComponent<FlightController>().speed *= 1.5f;
			other.gameObject.SetActive(false);
		} else if (other.gameObject.tag != "Player") {
			OnLose?.Invoke();
			Invoke(nameof(Lose), holdOnLose);
			fall = true;
		}
	}

	private void Win() {
		LevelManager.Instance.Win();
	}
	private void Lose() {
		LevelManager.Instance.Lose();
	}
}
