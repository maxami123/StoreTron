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
        Debug.Log("Trashbin interaction");
        var where = stocking.inventory.IndexOf(stocking.inventory[0]);
        stocking.inventory.Remove(stocking.inventory[0]);
        stocking.inventorySprites[where].enabled = false;

        // Finish Implementing this function
    }
}
