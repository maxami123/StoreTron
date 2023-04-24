using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour, IInteractable
{
    public GameObject menu;
    public GameObject storetron;

    public void Interact()
    {
        EnableMenu();
    }

    public void EnableMenu()
    {
        menu.SetActive(true);
        storetron.SetActive(false);
    }
    public void CloseMenu()
    {
        menu.SetActive(false);
        storetron.SetActive(true);
    }

    public void BuyObject(int index)
    {

    }
}
