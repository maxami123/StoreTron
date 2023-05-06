using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour, IInteractable
{
    public GameObject taskHandler;
    private AudioSource checkoutSfx;

    void Start()
    {
        checkoutSfx = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (taskHandler.GetComponent<TaskHandler>().line.Count > 0 && taskHandler.GetComponent<TaskHandler>().line[0].GetComponent<NavMeshAI>().inLine)
        {
            checkoutSfx.Play();
            taskHandler.GetComponent<TaskHandler>().line.RemoveAt(0);
            taskHandler.GetComponent<TaskHandler>().customersHelped++;
        }
    }
}
