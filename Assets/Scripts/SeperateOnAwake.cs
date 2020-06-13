using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperateOnAwake : MonoBehaviour
{
	private void Awake() {
		transform.SetParent(null);
	}
}
