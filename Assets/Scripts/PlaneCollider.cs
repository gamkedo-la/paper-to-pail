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

	private Vector3 pushPlane = new Vector3();
	private float pushMultiplier = 10;

	private Vector3 directPlane = new Vector3();
	private Vector3 planeRotation = new Vector3();
	private float directMultiplier = 0.5f;

	

	void Update() {
		//transform.position += pushPlane * pushMultiplier * Time.deltaTime; ;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + directPlane * directMultiplier * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (fall) {
			return;
		}

		if (other.gameObject.tag == "WinZone") {
			OnWin?.Invoke();
			Invoke(nameof(Win), holdOnWin);
			fall = true;
		} else if (other.gameObject.CompareTag("PushZone")) {
			Debug.Log("Enter Push");
			pushPlane += other.gameObject.transform.forward;
		} else if (other.gameObject.CompareTag("DirectZone")) {
			Debug.Log("Enter Direct");
			planeRotation = transform.rotation.eulerAngles;
			directPlane.x += directPlane.x - planeRotation.x;
			directPlane.y += directPlane.y - planeRotation.y;
			directPlane.z += directPlane.z - planeRotation.z;
			Debug.Log(directPlane);
		} else if (other.gameObject.CompareTag("Boost")) {
			gameObject.GetComponent<FlightController>().speed *= 1.5f;
			other.gameObject.SetActive(false);
		} else if (!other.gameObject.CompareTag("Player")) {
			OnLose?.Invoke();
			Invoke(nameof(Lose), holdOnLose);
			fall = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("PushZone")) {
			Debug.Log("Exit Push");
			pushPlane -= other.gameObject.transform.forward;
		} else if (other.gameObject.CompareTag("DirectZone")) {
			Debug.Log("Exit Direct");
			planeRotation = transform.rotation.eulerAngles;
			directPlane.x -= directPlane.x - planeRotation.x;
			directPlane.y -= directPlane.y - planeRotation.y;
			directPlane.z -= directPlane.z - planeRotation.z;
			Debug.Log(directPlane);
		}
	}

	private void Win() {
		LevelManager.Instance.Win();
	}
	private void Lose() {
		LevelManager.Instance.Lose();
	}
}
