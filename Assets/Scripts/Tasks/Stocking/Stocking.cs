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
    public List<GameObject> inventoryOptions;
    public List<Image> inventory1;
    public List<Image> inventory2;
    public List<Sprite> sprites;
    public List<Image> inventorySprites;

    // This will help determine whether the player can pick up anything else on top of what they currently have
    // This also gives me an idea about dropping items (Perhaps into a bin but that's for later)
    private int inventoryCap = 1;
    void Start()
    {
        // Figure out how large the player's inventory is
        inventoryCap = PlayerPrefs.GetInt("InventorySize");
        if (inventoryCap == 0)
        {
            inventoryCap = 1;
        }
        if (inventoryCap == 1)
        {
            inventorySprites = inventory1;
            if (inventoryOptions.Count > 1)
            {
                inventoryOptions[1].SetActive(false);
            }
        }
        else
        {
            inventorySprites = inventory2;
            inventoryOptions[0].SetActive(false);
        }
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
            inventory.Remove(index);

            if (where == 0 || inventorySprites.Count < 2)
            {
                inventorySprites[0].enabled = false;
            }
            else if (where == 0 && !inventorySprites[1].isActiveAndEnabled)
            {
                inventorySprites[0].enabled = false;
            }
            else 
            {
                int i;
                for (i = where; i <= inventorySprites.Count - 2; i++)
                {
                    inventorySprites[i].sprite = inventorySprites[i + 1].sprite;
                }
                inventorySprites[i].enabled = false;
            }

            return true;
        }
        return false;
    }
}
