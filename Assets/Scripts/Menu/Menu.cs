using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Button exit;
    public Button tutorial;
    public bool ExitMenuOpen = true;

    private void Start()
    {
        exit.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitMenuOpen == false)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                ExitMenuOpen = false;
                exit.gameObject.SetActive(false);
                tutorial.gameObject.SetActive(false);
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
        Debug.Log("test");
    }
}
