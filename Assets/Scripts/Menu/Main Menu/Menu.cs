using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{

    private void Start()
    {
        // Initialize Player Prefs
        PlayerPrefs.SetFloat("DashCooldown", 5f);
        PlayerPrefs.SetInt("InventorySize", 1);
        PlayerPrefs.SetFloat("Level1Clock", 0f);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
