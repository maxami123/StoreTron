using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button exitButton;
    public Button playButton;
    public AudioClip buttonSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Initialize Player Prefs
        PlayerPrefs.SetFloat("DashCooldown", 5f);
        PlayerPrefs.SetInt("InventorySize", 1);
        PlayerPrefs.SetFloat("Level1Clock", 0f);
        PlayerPrefs.SetInt("PrevLevel", 0);

        // Get the AudioSource component on this object
        audioSource = GetComponent<AudioSource>();

        // Add listeners to buttons
        exitButton.onClick.AddListener(Exit);
        playButton.onClick.AddListener(Play);
    }

    public void Exit()
    {
        // Play audio clip
        audioSource.PlayOneShot(buttonSound);

        Application.Quit();
    }

    public void Play()
    {
        // Play audio clip
        audioSource.PlayOneShot(buttonSound);

        SceneManager.LoadScene("Tutorial");
    }
}
