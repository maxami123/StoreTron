using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stocking : MonoBehaviour
{
    // This will be checked whenever seeing if something should be stocked
    public List<GameObject> inventory;

    // This will help determine whether the player can pick up anything else on top of what they currently have
    // This also gives me an idea about dropping items (Perhaps into a bin but that's for later)
    private int inventoryCap = 1;

    // Update is called once per frame
    void Update()
    {
        PickupObject();
        StockObject();
    }


    // Logic for interacting with the stock box
    void PickupObject()
    {
        // Need control check as well but I'll ignore for now

        // If statement will check to see if it was a box (since we don't have stock boxes I'm making it false for now).
        if (false)
        {
            // Make sure there is enough inventory space to pick up the object
            if (inventory.Count < inventoryCap)
            {
                // Add the gameobject to the inventory
            }
        }
    }

    // Logic for stocking a shelf (or whatever we would be stocking)
    void StockObject()
    {

        // Need control check as well but I'll ignore for now

        // Check if tile is interactable (Since this isn't important rn it will be false)
        if (false)
        {
            // Check to see if we contain the item to complete the task
            if (inventory.Contains(gameObject)) 
            {
                inventory.Remove(gameObject);
                // Call code to complete task on tile side
            }
        }
    }
}
