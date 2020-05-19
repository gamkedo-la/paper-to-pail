using UnityEngine;
using UnityEngine.Video;

public class IntroVideoHandler : MonoBehaviour {

	public VideoClip videoClip;
	public AudioClip introMusic;
	public AudioClip menuMusic;
	public float introEndTime;

	private VideoPlayer videoPlayer;
	public bool triggeredPlay = false;
	public bool started = false;

	void Start() {
		videoPlayer = gameObject.GetComponentInChildren<UnityEngine.Video.VideoPlayer>();
		videoPlayer.clip = videoClip;
	}

	void Update() {
		if (!videoPlayer.isPlaying && !triggeredPlay) {
			videoPlayer.Play();
			triggeredPlay = true;
		}
		if (videoPlayer.isPlaying && !started) {
			MusicManager.Instance.PlayTrack(introMusic, introEndTime);
			MusicManager.Instance.ScheduleTrack(menuMusic);
			//LevelManager.Instance.LoadTitle();
			started = true;
		}
		if (videoPlayer.isPaused && started) {
			//LevelManager.Instance.SetTitle();
			LevelManager.Instance.Title();
		}
	}
}