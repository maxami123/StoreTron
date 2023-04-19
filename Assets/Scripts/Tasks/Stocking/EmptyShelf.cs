using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyShelf : MonoBehaviour, IInteractable
{
    // Index will be the item id, for example (lettuce - 0, apple - 0, etc)
    public int index;
    public GameObject taskHandler;
    public void Interact()
    {
        var success = taskHandler.GetComponent<Stocking>().StockObject(index);
        if (success)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
