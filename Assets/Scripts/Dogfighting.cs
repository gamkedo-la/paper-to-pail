using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogfighting : MonoBehaviour {

	public GameObject shotPrefab;

	public AudioClip[] shotSoundEffect;
	private AudioSource audioSource;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {

        if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0) && Time.timeScale > 0) {
            GameObject newShot = Instantiate(shotPrefab);
            newShot.transform.position = gameObject.transform.position;
            newShot.transform.rotation = gameObject.transform.rotation;
            newShot.GetComponent<Shot>().ownerTag = "Player";
			
			AudioClip sfxClip = shotSoundEffect[Random.Range(0, shotSoundEffect.Length)];
			audioSource.PlayOneShot(sfxClip, 0.25f);
		}
    }
}