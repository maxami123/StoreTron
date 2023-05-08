using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbinController : MonoBehaviour, IInteractable
{
    public Stocking stocking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {
        if (stocking.inventory.Count <= 0) { return; }
        Debug.Log("Trashbin interaction");
        //var where = stocking.inventory.IndexOf(stocking.inventory[0]);
        var where = stocking.inventory.IndexOf(stocking.inventory[stocking.inventory.Count - 1]);

        stocking.inventory.Remove(stocking.inventory[0]);
        stocking.inventorySprites[where].enabled = false;

        // Finish Implementing this function
        /*
        stocking.inventory.Remove(stocking.inventory[0]);
        if (where == 0 || stocking.inventorySprites.Count < 2)
        {
            stocking.inventorySprites[0].enabled = false;
        }
        else if (where == 0 && !stocking.inventorySprites[1].isActiveAndEnabled)
        {
            stocking.inventorySprites[0].enabled = false;
        }
        else
        {
            int i;
            for (i = where; i <= stocking.inventorySprites.Count - 2; i++)
            {
                stocking.inventorySprites[i].sprite = stocking.inventorySprites[i + 1].sprite;
            }
            stocking.inventorySprites[i].enabled = false;
        }
        */
    }
}
