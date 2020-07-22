using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIlogic : MonoBehaviour {

	public Transform target;
	public Transform myPlaneModel;
	private float altitudeThreshold = 2;
	private float turnThreshold = 4;
	private float turnSpeed = 120;
	private float speedNow = 20;
	private float maxSpeed = 30;
	private float minSpeed = 10;
	private float slowSpeedTurnBonus = 3;
	private float slowDownPerc = 0.15f;
	private float speedUpPerc = 0.1f;

	public AudioClip[] rustleSoundEffects;
	public AudioClip[] collisionSoundEffects;
	private AudioSource audioSource;
	public float audioTimeOut = 0;

	private float planeRoll = 0.0f;
	private float planePitch = 0.0f;
	private Quaternion initialModelRotation = Quaternion.identity;


	//Things to do
	//wall check, crash avoidance
	//avoid player crash
	//handle crashing
	//shoot when player in range
	//

	void Start() {
		initialModelRotation = myPlaneModel.rotation;
		LevelManager.Instance.AddGoal();

		audioSource = GetComponent<AudioSource>();
	}

	void Update() {


		audioSource.volume = speedNow / 40f;

		if (Time.time >= audioTimeOut) {
			AudioClip sfxClip = rustleSoundEffects[Random.Range(0, rustleSoundEffects.Length)];
			audioSource.PlayOneShot(sfxClip);

			float delay = Random.Range(0, 80 / speedNow - 1);
			audioTimeOut = sfxClip.length + delay + Time.time;
		}
	}

	void FixedUpdate() {

		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		Vector3 inMyView = transform.InverseTransformPoint(target.position);
		//Debug.Log(inMyView.x + " " + inMyView.y);
		if (Mathf.Abs(inMyView.y) > altitudeThreshold) {
			planeRoll = 0f;
			float newY = Mathf.Lerp(transform.position.y, target.position.y, 0.1f);
			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
			if (inMyView.y > 0) {
				planePitch = 30.0f;
			} else {
				planePitch = -30f;
			}
		} else {
			planePitch = 0f;
			Vector3 positionAtMyHeight = target.position;
			positionAtMyHeight.y = transform.position.y;
			float angleDif = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(positionAtMyHeight - transform.position));
			if (angleDif > turnThreshold) {
				float turnForSpeedNow = turnSpeed + (maxSpeed - speedNow) * slowSpeedTurnBonus;
				if (inMyView.x > 0) {
					planeRoll = -45f;
					transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
				} else {
					planeRoll = 45f;
					transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
				}
			}
		}
		if (inMyView.z < 0) {
			speedNow = Mathf.Lerp(speedNow, minSpeed, slowDownPerc);
		} else {
			speedNow = Mathf.Lerp(speedNow, maxSpeed, speedUpPerc);
		}
		//Debug.Log(speedNow);
		transform.position += transform.forward * speedNow * Time.deltaTime;
		Quaternion modelRot = transform.rotation;
		modelRot *= initialModelRotation;
		modelRot *= Quaternion.AngleAxis(planeRoll, Vector3.up);
		modelRot *= Quaternion.AngleAxis(planePitch, Vector3.right);
		myPlaneModel.rotation = Quaternion.Slerp(myPlaneModel.rotation, modelRot, 0.05f);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Shot")) {
			LevelManager.Instance.RemoveGoal();
			Destroy(gameObject);
		}
	}

	private void OnDestroy() {
		AudioClip sfxClip = collisionSoundEffects[Random.Range(0, collisionSoundEffects.Length)];
		AudioSource.PlayClipAtPoint(sfxClip, gameObject.transform.position, 8.0f);
	}
}
