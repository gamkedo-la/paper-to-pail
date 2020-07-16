using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Slider volumeSlider = null;
    [SerializeField]
    private Text volumeLabel = null;

    private bool gamePaused = false;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat( "Volume", 1.0f );
        volumeSlider.value = AudioListener.volume;
        volumeLabel.text = $"Volume: {AudioListener.volume * 100:0}%";
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

    public void SetVolume( float volume )
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat( "Volume", volume );

        volumeSlider.value = AudioListener.volume;
        volumeLabel.text = $"Volume: {AudioListener.volume * 100:0}%";
    }

    private void Pause()
    {
        gamePaused = true;
        animator.SetTrigger( "Show" );
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Resume()
    {
        gamePaused = false;
        animator.SetTrigger( "Hide" );
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
