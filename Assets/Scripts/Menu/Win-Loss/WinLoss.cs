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


    private void Start()
    {
        clockTime = PlayerPrefs.GetInt("Level1Clock");
        SelectMedal();
        ShowTimeText();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
