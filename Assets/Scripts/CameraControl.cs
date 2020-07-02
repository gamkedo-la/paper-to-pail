using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public bool locked = false;
	public LayerMask CamOcclusion;

	[SerializeField] float joystickDeadZone = 0.1f;

	float cameraOffsetX = 0f;
	float cameraOffsetY = 0f;
	Vector3 oldView = new Vector3();

	void Update() {
		if (locked) {
			LockedCamera();
		} else {
			FollowCamera();
		}
	}

	public void SetCameraLock(bool setLock) {
		locked = setLock;
	}

	private void FollowCamera() {

		cameraOffsetX += Input.GetAxis("MouseX") / 5;
		float joystickHorizontal = Input.GetAxis("Horizontal");
		if ( Mathf.Abs( joystickHorizontal ) >= joystickDeadZone )
		{
			cameraOffsetX += joystickHorizontal / 5;
		}

		if (cameraOffsetX > 30) { cameraOffsetX = 30; }
		if (cameraOffsetX < -30) { cameraOffsetX = -30; }

		cameraOffsetY += Input.GetAxis("MouseY") / 5;
		float joystickVertical = Input.GetAxis("Vertical");
		if ( Mathf.Abs( joystickVertical ) >= joystickDeadZone )
		{
			cameraOffsetY += joystickVertical / 5;
		}

		if (cameraOffsetY > 30) { cameraOffsetY = 30; }
		if (cameraOffsetY < -30) { cameraOffsetY = -30; }

		Vector3 moveCamTo = transform.position - transform.forward * 10f + Vector3.up * 5 - Vector3.up * cameraOffsetY / 4 - transform.right * cameraOffsetX / 4;
		float bias = 0.95f;
		var targetPosition = Camera.main.transform.position * bias + moveCamTo * (1f - bias);
		
		// Try not to look through walls.
		RaycastHit wallHit;
		if (Physics.Linecast(transform.position, targetPosition, out wallHit, CamOcclusion))
		{
			targetPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, targetPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
		}

		Camera.main.transform.position = targetPosition;
		Vector3 moveViewTo = transform.position + transform.forward * 30f + transform.right * cameraOffsetX + Vector3.up * cameraOffsetY / 3;
		Camera.main.transform.LookAt(moveViewTo);
		oldView = moveViewTo;

	}

	private void LockedCamera() {
		float bias = 0.99f;
		Vector3 moveViewTo = oldView * bias + transform.position * (1f - bias);
		Camera.main.transform.LookAt(moveViewTo);
		oldView = moveViewTo;
	}

}
