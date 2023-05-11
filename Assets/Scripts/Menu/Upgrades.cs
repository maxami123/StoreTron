using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject dashButton;
    public AudioClip buttonSound;

    private int prevLevel;
    private int inventorySize;
    private float dashCooldown;

    private AudioSource audioSource;

    private void Start()
    {
        prevLevel = PlayerPrefs.GetInt("PrevLevel");
        dashCooldown = PlayerPrefs.GetFloat("DashCooldown");
        inventorySize = PlayerPrefs.GetInt("InventorySize");

        // Get the AudioSource component on this object
        audioSource = GetComponent<AudioSource>();

        if (dashCooldown == 2f)
        {
            dashButton.SetActive(false);
        }
        else if (inventorySize == 2)
        {
            inventoryButton.SetActive(false);
        }
    }

    public void IncreaseInventory()
    {
        PlayerPrefs.SetInt("InventorySize", 2);

        // Play audio clip
        audioSource.PlayOneShot(buttonSound);

        SceneManager.LoadScene($"Level {prevLevel + 1}");
    }

    public void DecreaseDashCooldown()
    {
        PlayerPrefs.SetFloat("DashCooldown", 2f);

        // Play audio clip
        audioSource.PlayOneShot(buttonSound);

        SceneManager.LoadScene($"Level {prevLevel + 1}");
    }
}
