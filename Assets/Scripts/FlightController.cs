using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {
	public float speed = 20f;
	public float bank = 50f;
	public float pull = 30f;
	public float pickup = 10f;

	float gravity = 0.0001f;

	void Update() {
		Vector3 moveCamTo = transform.position - transform.forward * 10f + Vector3.up * 5;
		float bias = 0.95f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1f - bias);
		Camera.main.transform.LookAt(transform.position + transform.forward * 30f);

		transform.position += transform.forward * Time.deltaTime * speed+ Vector3.down * gravity * Time.deltaTime;

		speed -= transform.forward.y * Time.deltaTime * pickup;

		transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * pull, Input.GetAxis("Horizontal") * Time.deltaTime * bank, 0f);

	}
}
