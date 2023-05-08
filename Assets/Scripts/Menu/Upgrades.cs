using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject dashButton;

    private int prevLevel;
    private int inventorySize;
    private float dashCooldown;
    private void Start()
    {
        prevLevel = PlayerPrefs.GetInt("PrevLevel");
        dashCooldown = PlayerPrefs.GetFloat("DashCooldown");
        inventorySize = PlayerPrefs.GetInt("InventorySize");
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
        SceneManager.LoadScene($"Level {prevLevel+1}");
    }

    public void DecreaseDashCooldown()
    {
        PlayerPrefs.SetFloat("DashCooldown", 2f);
        SceneManager.LoadScene($"Level {prevLevel+1}");
    }
}
