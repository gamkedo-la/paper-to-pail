using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlaneCollider : MonoBehaviour {

	public UnityEvent OnWin = null;
	public UnityEvent OnLose = null;

	public float holdOnWin = 3.0f;
	public float holdOnLose = 1.5f;

	public AudioClip[] collisionSoundEffects;
	private AudioSource audioSource;

	private bool fall = false;

	private Vector3 pushPlane = new Vector3();
	private float pushMultiplier = 10;

	private Quaternion directPlane = new Quaternion();
	private int currentDirectZones = 0;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		transform.position += pushPlane * pushMultiplier * Time.deltaTime; ;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, directPlane, 270f * Time.deltaTime);
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
			pushPlane += other.gameObject.transform.forward;

		} else if (other.gameObject.CompareTag("DirectZone")) {
			FlightController flight = gameObject.GetComponent<FlightController>();
			AudioClip sfxClip = flight.rustleSoundEffects[Random.Range(0, flight.rustleSoundEffects.Length)];
			audioSource.PlayOneShot(sfxClip);

			directPlane = other.transform.rotation;
			currentDirectZones++;

		} else if (other.gameObject.CompareTag("BoostZone")) {
			gameObject.GetComponent<FlightController>().speed *= 1.1f;

		} else if (other.gameObject.CompareTag("Boost")) {
			FlightController flight = gameObject.GetComponent<FlightController>();
			AudioClip sfxClip = flight.rustleSoundEffects[Random.Range(0, flight.rustleSoundEffects.Length)];
			audioSource.PlayOneShot(sfxClip);

			gameObject.GetComponent<FlightController>().speed *= 1.5f;
			other.gameObject.SetActive(false);

		} else if (other.gameObject.CompareTag("Loop")) {
			other.gameObject.GetComponent<Loop>().Complete();

		} else if (other.gameObject.CompareTag("Pickup")) {
			other.gameObject.GetComponent<Pickup>().Complete();

		} else if (other.gameObject.CompareTag("Enemy")) {


		} else if (other.gameObject.CompareTag("Shot")) {


		} else if (!other.gameObject.CompareTag("Player")) {
			OnLose?.Invoke();
			Invoke(nameof(Lose), holdOnLose);
			fall = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("PushZone")) {
			pushPlane -= other.gameObject.transform.forward;
		} else if (other.gameObject.CompareTag("DirectZone")) {
			currentDirectZones--;
			if (currentDirectZones <= 0) {
				directPlane = new Quaternion();
			}
		}
	}

	private void Win() {
		LevelManager.Instance.Win();
	}
	private void Lose() {
		DontDestroyOnLoad(this.gameObject);
		GetComponent<Rigidbody>().isKinematic = false;
		LevelManager.Instance.Lose();
	}

	public void PlayCollisionSFX()
	{
		AudioClip sfxClip = collisionSoundEffects[Random.Range(0, collisionSoundEffects.Length)];
		audioSource.Stop();
		audioSource.PlayOneShot(sfxClip);
	}
}
