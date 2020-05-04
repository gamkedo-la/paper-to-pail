using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlaneCollider : MonoBehaviour
{
	public UnityEvent OnWin = null;

	private bool won = false;

	private void OnTriggerEnter(Collider other) {
		if (won) {
			return;
		}

		if (other.gameObject.tag == "Trashcan") {
			OnWin?.Invoke();
			Invoke(nameof(Win), 3f);
			won = true;
		} else if (other.gameObject.tag == "Boost") {
			gameObject.GetComponent<FlightController>().speed *= 1.5f;
			other.gameObject.SetActive(false);
		} else if (other.gameObject.tag != "Player") {
			SceneManager.LoadScene("Loose");
		}
	}

	private void Win()
	{
		SceneManager.LoadScene( "Win" );
	}
}
