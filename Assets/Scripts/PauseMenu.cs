using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool gamePaused = false;

    [SerializeField]
    private GameObject pauseMenuCanvas = null;

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
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Resume()
    {
        gamePaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
