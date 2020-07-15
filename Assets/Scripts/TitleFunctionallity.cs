using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFunctionallity : MonoBehaviour
{
	public void ToGame() {
		MusicManager.Instance.nextTrack();
		LevelManager.Instance.LoadLevel();
	}

	public void ToCredits() {
		LevelManager.Instance.Credits();
	}
}
