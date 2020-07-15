using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

	public static MusicManager Instance;
	public AudioSource musicSourcePrefab;
	public AudioClip scheduledTrack;

	public AudioSource currentTrack;
	public double timeNow;
	public double startTime;
	public double endTime;

	public bool manualLoop = false;
	public bool nothingSchedualled = false;
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
		timeNow = AudioSettings.dspTime;

		if (currentTrack != null && scheduledTrack != null && manualLoop && nothingSchedualled && endTime <= timeNow - fadeTime) { //Manual loop and schedualled track
			PlayTrackSchedualled(scheduledTrack, endTime, false);
			nothingSchedualled = false;
		} else if (currentTrack != null && scheduledTrack != null && endTime <= timeNow) { //Autolooping and schedualled track
			PlayTrack(scheduledTrack, true);
			nothingSchedualled = false;
		} else if (currentTrack != null && manualLoop && endTime <= timeNow) { //Manual looping, none schedualled
			PlayTrack(currentTrack.clip, endTime, false);
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

		startTime = AudioSettings.dspTime;
		endTime = startTime + (double)currentTrack.clip.samples/currentTrack.clip.frequency;
		manualLoop = false;
		fadeOnEnd = fadeOut;

		currentTrack.Play();
		scheduledTrack = null;
	}

	public void PlayTrack(AudioClip newTrack, double trackEndTime, bool fadeOut = true) {
		if (currentTrack != null && fadeOnEnd) {
			StartCoroutine(FadeOutAndStop(currentTrack, fadeTime));
			currentTrack.gameObject.name = currentTrack.gameObject.name + "(old)";
		}
		currentTrack = Instantiate(musicSourcePrefab).GetComponent<AudioSource>();
		currentTrack.gameObject.transform.parent = gameObject.transform;
		currentTrack.clip = newTrack;
		currentTrack.loop = false;
		currentTrack.gameObject.name = "Music Track";

		startTime = AudioSettings.dspTime;
		endTime = startTime + trackEndTime;
		manualLoop = true;
		fadeOnEnd = fadeOut;

		currentTrack.Play();
		scheduledTrack = null;
	}

	private void PlayTrackSchedualled(AudioClip newTrack, double schedual, bool fadeOut = false) {
		if (currentTrack != null && fadeOnEnd) {
			StartCoroutine(FadeOutAndStop(currentTrack, fadeTime));
			currentTrack.gameObject.name = currentTrack.gameObject.name + "(old)";
		} else if (currentTrack != null) {
			currentTrack.gameObject.name = currentTrack.gameObject.name + "(old)";
		}


		currentTrack = Instantiate(musicSourcePrefab).GetComponent<AudioSource>();
		currentTrack.gameObject.transform.parent = gameObject.transform;
		currentTrack.clip = newTrack;
		currentTrack.loop = false;
		currentTrack.gameObject.name = "Music Track";

		startTime = AudioSettings.dspTime;
		endTime = startTime + (double)currentTrack.clip.samples/currentTrack.clip.frequency;
		manualLoop = true;
		fadeOnEnd = fadeOut;

		currentTrack.PlayScheduled(schedual);
		nothingSchedualled = true;
		scheduledTrack = null;
	}

	public void ScheduleTrack(AudioClip newTrack) {
		if (currentTrack != null) {
			scheduledTrack = newTrack;
		} else {
			PlayTrack(scheduledTrack);
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

	//Hard cody stuff that you shouldn't do this way
	public AudioClip[] gameplayMusic = new AudioClip[4];
	public int currentIndex = 1;

	public void nextTrack() {
		int newIndex = Random.Range(0, 3);
		while (newIndex == currentIndex) {
			newIndex = Random.Range(0, 3);
		}
		currentIndex = newIndex;

		switch(currentIndex) {
			case 0:
				PlayTrack(gameplayMusic[0]);
				ScheduleTrack(gameplayMusic[1]);
				break;
			case 1:
				PlayTrack(gameplayMusic[2]);
				break;
			case 2:
				PlayTrack(gameplayMusic[3]);
				break;
		}
	}

}

