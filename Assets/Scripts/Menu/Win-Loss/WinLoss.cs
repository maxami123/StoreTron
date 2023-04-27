using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{
    public Button exitButton;
    public Button restartButton;
    public GameObject winLossUI;

    void Start()
    {
        exitButton.onClick.AddListener(ExitToMainMenu);
        restartButton.onClick.AddListener(Restart);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            winLossUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
