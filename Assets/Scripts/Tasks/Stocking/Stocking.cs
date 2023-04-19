using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stocking : MonoBehaviour
{
    // This will contain indices that we can map to grocery sprites and shelve sprites
    public List<int> inventory;

    // This will help determine whether the player can pick up anything else on top of what they currently have
    // This also gives me an idea about dropping items (Perhaps into a bin but that's for later)
    private int inventoryCap = 1;

    // Update is called once per frame
    void Update()
    {
        //PickupObject();
        //StockObject();
    }


    // Logic for interacting with the stock box
    public bool PickupObject(int index)
    {
        // Make sure there is enough inventory space to pick up the object
        if (inventory.Count < inventoryCap)
        {
        // Add the gameobject to the inventory
        inventory.Add(index);

        // If successful return true
        return true;
        }
        // If nothing got added then return false
        return false;
    }

    // Logic for stocking a shelf (or whatever we would be stocking)
    public bool StockObject(int index)
    {
        Debug.Log(inventory.Contains(index));
            // Check to see if we contain the item to complete the task
        if (inventory.Contains(index)) 
        {
            inventory.Remove(index);
            return true;
        }
        return false;
    }
}
