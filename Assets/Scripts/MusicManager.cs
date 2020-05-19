using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public static MusicManager Instance;
	public AudioSource musicSourcePrefab;
	public AudioClip scheduledTrack;

	public AudioSource currentTrack;
	public float endTime;
	public bool manualLoop;

	public float fadeTime = 0.25f;
	public bool fadeOnEnd = true;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Update() {

		if (currentTrack != null && manualLoop && scheduledTrack != null && endTime <= currentTrack.time) {
			MusicManager.Instance.PlayTrack(scheduledTrack, false);
			scheduledTrack = null;
		} else if (currentTrack != null && scheduledTrack != null && endTime <= currentTrack.time) {
			MusicManager.Instance.PlayTrack(scheduledTrack, true);
			scheduledTrack = null;
		} else if (currentTrack != null && manualLoop && endTime <= currentTrack.time) {
			MusicManager.Instance.PlayTrack(currentTrack.clip, endTime, false);
		}
	}

	public void PlayTrack(AudioClip newTrack, bool fadeOut = true) {
		if (currentTrack != null && fadeOnEnd) {
			StartCoroutine(FadeOutAndStop(currentTrack, fadeTime));
			currentTrack.gameObject.name = currentTrack.gameObject.name + "(old)";
		}
		currentTrack = Instantiate(musicSourcePrefab).GetComponent<AudioSource>();
		currentTrack.gameObject.transform.parent = gameObject.transform;
		currentTrack.clip = newTrack;
		currentTrack.loop = true;
		currentTrack.gameObject.name = "Music Track";

		endTime = currentTrack.clip.length;
		manualLoop = false;
		fadeOnEnd = fadeOut;

		currentTrack.Play();
	}

	public void PlayTrack(AudioClip newTrack, float trackEndTime, bool fadeOut = true) {
		if (currentTrack != null && fadeOnEnd) {
			StartCoroutine(FadeOutAndStop(currentTrack, fadeTime));
			currentTrack.gameObject.name = currentTrack.gameObject.name + "(old)";
		}
		currentTrack = Instantiate(musicSourcePrefab).GetComponent<AudioSource>();
		currentTrack.gameObject.transform.parent = gameObject.transform;
		currentTrack.clip = newTrack;
		currentTrack.loop = false;
		currentTrack.gameObject.name = "Music Track";

		endTime = trackEndTime;
		manualLoop = true;
		fadeOnEnd = fadeOut;

		currentTrack.Play();
	}

	public void ScheduleTrack(AudioClip newTrack) {
		if (currentTrack != null) {
			scheduledTrack = newTrack;
		} else {
			MusicManager.Instance.PlayTrack(scheduledTrack);
		}
	}

	IEnumerator FadeOutAndStop(AudioSource source, float fadeTime) {
		float startTime = Time.time;
		float currentTime;
		float startVolume = source.volume;

		while (startTime + fadeTime > Time.time) {
			currentTime = Time.time - startTime;

			source.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeTime);
			yield return null;
		}

		source.Stop();
		Destroy(source.gameObject);

	}

	IEnumerator FadeOut(AudioSource source, float fadeTime) {
		float startTime = Time.time;
		float currentTime;
		float startVolume = source.volume;

		while (startTime + fadeTime > Time.time) {
			currentTime = Time.time - startTime;

			source.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeTime);
			yield return null;
		}

		source.volume = 0f;

	}

	IEnumerator FadeIn(AudioSource source, float fadeTime) {
		float startTime = Time.time;
		float currentTime;
		float startVolume = source.volume;

		while (startTime + fadeTime > Time.time) {
			currentTime = Time.time - startTime;

			source.volume = Mathf.Lerp(startVolume, 1f, currentTime / fadeTime);
			yield return null;
		}

		source.volume = 1f;

	}
}

