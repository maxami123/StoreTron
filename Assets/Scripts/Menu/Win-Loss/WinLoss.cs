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

    private int clockTime;
    private int prevLevel;


    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Win Screen"))
        {
            clockTime = PlayerPrefs.GetInt("Level1Clock");
            prevLevel = PlayerPrefs.GetInt("PrevLevel");
            SelectMedal();
            ShowTimeText();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene($"Level {prevLevel}");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel()
    {
        // Check to see if the next level exists in the build or not
        if (prevLevel == 2)
        {
            return;
        }
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
