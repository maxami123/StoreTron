using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class WinLoss : MonoBehaviour
{
    public GameObject levelCompletedText;
    public GameObject personalBestText;
    public List<int> medalTimes;
    public List<GameObject> medals;
    public AudioClip buttonClickSound;

    private int clockTime;
    private int prevLevel;
    private AudioSource audioSource;

    void Start()
    {
        prevLevel = PlayerPrefs.GetInt("PrevLevel");
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Win Screen") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Final Win Screen"))
        {
            clockTime = PlayerPrefs.GetInt("Level1Clock");
            SelectMedal();
            ShowTimeText();
        }
        audioSource = GetComponent<AudioSource>();

        // Add listeners to all buttons to play sound when clicked
        Button playAgainButton = GameObject.Find("Play Again Button").GetComponent<Button>();
        playAgainButton.onClick.AddListener(PlayAgain);

        Button mainMenuButton = GameObject.Find("Main Menu Button").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(MainMenu);
        if (SceneManager.GetActiveScene().name == "Win Screen")
        {
            Button nextLevelButton = GameObject.Find("Next Level Button").GetComponent<Button>();
            nextLevelButton.onClick.AddListener(NextLevel);
        }
    }

    public void PlayAgain()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene($"Level {prevLevel}");

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void NextLevel()
    {
        // Check to see if the next level exists in the build or not
        if (prevLevel == 2)
        {
            return;
        }
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("Upgrades");

    }

    void SelectMedal()
    {
        if (clockTime < medalTimes[0])
        {
            medals[0].SetActive(true);
        }
        else if (clockTime < medalTimes[1])
        {
            medals[1].SetActive(true);
        }
        else
        {
            medals[2].SetActive(true);
        }
    }

    void ShowTimeText()
    {
        levelCompletedText.GetComponent<TextMeshProUGUI>().text = $"Level completed in {clockTime} seconds";
        personalBestText.GetComponent<TextMeshProUGUI>().text = $"Personal Best: {clockTime} seconds";
    }
}
