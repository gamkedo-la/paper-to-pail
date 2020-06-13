using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool gamePaused = false;

    [SerializeField]
    private GameObject pauseMenuCanvas;

    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (gamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        gamePaused = true;
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        gamePaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
