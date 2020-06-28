using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	public string ownerTag = "";
	public float speed = 50;

	private void Update() {
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other) {
		if (!other.gameObject.CompareTag(ownerTag)) {
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
	}
}
