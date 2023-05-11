using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button exitButton;
    public Button continueButton;
    public Button restartButton;
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public AudioClip buttonSound;

    void Start()
    {
        exitButton.onClick.AddListener(Exit);
        continueButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);

        AudioSource audioSource = GetComponent<AudioSource>();
        exitButton.onClick.AddListener(() => PlayButtonSound(audioSource));
        continueButton.onClick.AddListener(() => PlayButtonSound(audioSource));
        restartButton.onClick.AddListener(() => PlayButtonSound(audioSource));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void PlayButtonSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
