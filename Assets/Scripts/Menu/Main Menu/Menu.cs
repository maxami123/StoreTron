using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
