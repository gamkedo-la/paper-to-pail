using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogfighting : MonoBehaviour {

	public GameObject shotPrefab;

    void Update() {

        if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) {
            GameObject newShot = Instantiate(shotPrefab);
            newShot.transform.position = gameObject.transform.position;
            newShot.transform.rotation = gameObject.transform.rotation;
            newShot.GetComponent<Shot>().ownerTag = "Player";
        }
    }
}