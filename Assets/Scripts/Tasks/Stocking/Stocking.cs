using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Stocking : MonoBehaviour
{
    // This will contain indices that we can map to grocery sprites and shelve sprites
    public List<int> inventory;
    public List<Image> inventorySprites;
    public List<Sprite> sprites;

    // This will help determine whether the player can pick up anything else on top of what they currently have
    // This also gives me an idea about dropping items (Perhaps into a bin but that's for later)
    private int inventoryCap = 1;

    void Start()
    {
        foreach(var imageSprite in inventorySprites)
        {
            imageSprite.enabled = false;
        }
    }


    // Logic for interacting with the stock box
    public bool PickupObject(int index)
    {
        // Make sure there is enough inventory space to pick up the object
        if (inventory.Count < inventoryCap)
        {
            // Add the gameobject to the inventory
            inventory.Add(index);
            var tempSprite = inventorySprites[inventory.Count() - 1];
            tempSprite.enabled = true;
            tempSprite.sprite = sprites[index];
            
            // If successful return true
            return true;
        }
        // If nothing got added then return false
        return false;
    }

    // Logic for stocking a shelf (or whatever we would be stocking)
    public bool StockObject(int index)
    {
            // Check to see if we contain the item to complete the task
        if (inventory.Contains(index)) 
        {
            var where = inventory.IndexOf(index);
            inventory.Remove(where);
            inventorySprites[where].enabled = false;
            return true;
        }
        return false;
    }
}
