using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour, IInteractable
{
    public GameObject taskHandler;

    public void Interact()
    {
        if (taskHandler.GetComponent<TaskHandler>().line.Count > 0)
        {
            taskHandler.GetComponent<TaskHandler>().line.RemoveAt(0);
            taskHandler.GetComponent<TaskHandler>().customersHelped++;
        }
    }
}
