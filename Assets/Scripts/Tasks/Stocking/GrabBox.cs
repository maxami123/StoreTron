using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBox : MonoBehaviour, IInteractable
{
    public GameObject taskHandler;

    private Stocking stocking;
    void Start()
    {
        stocking = taskHandler.GetComponent<Stocking>();
    }

    public void Interact()
    {
        var success = stocking.PickupObject(0);
        if (success)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
