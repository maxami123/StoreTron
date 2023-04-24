using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBox : MonoBehaviour, IInteractable
{
    public GameObject taskHandler;
    public int index;
    private Stocking stocking;
    void Start()
    {
        gameObject.tag = "Interactable";
        taskHandler = GameObject.FindGameObjectWithTag("GameController");
        stocking = taskHandler.GetComponent<Stocking>();
    }

    public void Interact()
    {
        var success = stocking.PickupObject(index);
        if (success)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
