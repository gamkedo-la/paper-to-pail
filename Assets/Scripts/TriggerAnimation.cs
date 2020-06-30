using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour {

	public Animator animator;
	public string triggerName;

	public float triggerDistance;

	private GameObject thePlayer;

	private void Start() {
		thePlayer = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update() {
		if (Vector3.Distance(thePlayer.transform.position, gameObject.transform.position) <= triggerDistance)
			animator.SetBool(triggerName, true);
	}
}
