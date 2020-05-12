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
		transform.position += transform.forward * Time.deltaTime * speed+ Vector3.down * gravity * Time.deltaTime;

		speed -= transform.forward.y * Time.deltaTime * pickup;

		transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * pull, Input.GetAxis("Horizontal") * Time.deltaTime * bank, 0f);

	}
}
