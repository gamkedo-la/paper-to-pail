using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFunctionallity : MonoBehaviour
{
	public void ToGame() {
		LevelManager.Instance.LoadLevel();
		MusicManager.Instance.nextTrack();
	}

	public void SetGameType(int gameType)
	{
		LevelManager.Instance.currentGameType = (GameType) gameType;
	}

	public void ToCredits() {
		LevelManager.Instance.Credits();
	}
}
