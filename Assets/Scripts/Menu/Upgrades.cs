using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public void IncreaseInventory()
    {
        PlayerPrefs.SetInt("InventorySize", 2);
        SceneManager.LoadScene("Level 1");
    }

    public void DecreaseDashCooldown()
    {
        PlayerPrefs.SetFloat("DashCooldown", 2f);
        SceneManager.LoadScene("Level 1");
    }
}
