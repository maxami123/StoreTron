using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskHandler : MonoBehaviour
{
    private List<EmptyShelf> lettuceShelves;
    private List<EmptyShelf> appleShelves;

    public GameObject lettuceParent;
    public GameObject appleParent;

    private void Start()
    {
        // Convert these gameobjects into a list of their children
        lettuceShelves = lettuceParent.GetComponentsInChildren<EmptyShelf>().ToList();
        appleShelves = appleParent.GetComponentsInChildren<EmptyShelf>().ToList();

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
            SceneManager.LoadScene("Upgrades");
        }
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
        return false;
    }
}
