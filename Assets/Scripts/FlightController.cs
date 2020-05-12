using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {
	public float speed = 20f;
	public float bank = 50f;
	public float pull = 30f;
	public float pickup = 10f;

	float gravity = 0.0001f;

	float cameraOffsetX = 0f;
	float cameraOffsetY = 0f;

	void Update() {
		cameraOffsetX += Input.GetAxis("MouseX");
		if (cameraOffsetX > 30) { cameraOffsetX = 30; }
		if (cameraOffsetX < -30) { cameraOffsetX = -30; }

		cameraOffsetY += Input.GetAxis("MouseY");
		if (cameraOffsetY > 30) { cameraOffsetY = 30; }
		if (cameraOffsetY < -30) { cameraOffsetY = -30; }

		Vector3 moveCamTo = transform.position - transform.forward * 10f + Vector3.up * 5 - Vector3.up * cameraOffsetY/4 - transform.right * cameraOffsetX/4;
		float bias = 0.95f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1f - bias);
		Camera.main.transform.LookAt(transform.position + transform.forward * 30f + transform.right * cameraOffsetX + Vector3.up * cameraOffsetY/3);

		transform.position += transform.forward * Time.deltaTime * speed+ Vector3.down * gravity * Time.deltaTime;

		speed -= transform.forward.y * Time.deltaTime * pickup;

		transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * pull, Input.GetAxis("Horizontal") * Time.deltaTime * bank, 0f);

	}
}
