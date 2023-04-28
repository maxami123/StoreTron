using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Metadata;

public class TaskHandler : MonoBehaviour
{
    private List<EmptyShelf> lettuceShelves;
    private List<EmptyShelf> appleShelves;
    private float clock;

    public GameObject lettuceParent;
    public GameObject appleParent;
    public List<GameObject> line;
    public int customersInLevel = 6;
    public int customersHelped = 0;

    private void Start()
    {
        clock = 0f;
        // Convert these gameobjects into a list of their children
        lettuceShelves = lettuceParent.GetComponentsInChildren<EmptyShelf>(true).ToList();
        appleShelves = appleParent.GetComponentsInChildren<EmptyShelf>(true).ToList();

        foreach (var shelf in lettuceShelves)
        {
            if (shelf.GetComponent<SpriteRenderer>() != null)
            {
                lettuceShelves.Remove(shelf);
            }
        }

        foreach (var shelf in appleShelves)
        {
            if (shelf.GetComponent<SpriteRenderer>() != null)
            {
                appleShelves.Remove(shelf);
            }
        }

        // Initialize these variables required for the gameobjects to work
        foreach (var script in lettuceShelves)
        {
            script.index = 0;
        }
        foreach (var script in appleShelves)
        {
            script.index = 1;
        }
    }

    private void Update()
    {
        bool stocked = ShelvesStocked();
        bool helped = CustomersHelped();

        if (stocked && helped) 
        {
            SceneManager.LoadScene("Win Screen");
            PlayerPrefs.SetInt("Level1Clock", (int)Math.Round(clock));
        }
        clock += Time.deltaTime;
    }

    // Determine whether or not all the shelves are stocked
    private bool ShelvesStocked()
    {
        bool lettuceEmpty = true;
        bool appleEmpty = true;

        // Loop through both the lists to see if the objects are enabled if they aren't then we'll load the upgrades scene
        foreach (var script in lettuceShelves)
        {
            if (script.gameObject.activeSelf)
            {
                lettuceEmpty = false;
            }
        }
        foreach (var script in appleShelves)
        {
            if (script.gameObject.activeSelf)
            {
                appleEmpty = false;
            }
        }
        if (lettuceEmpty && appleEmpty)
        {
            return true;
        }
        return false;
    }

    private bool CustomersHelped()
    {
        if (customersHelped >= customersInLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
