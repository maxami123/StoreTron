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
    private int trackWaves = 0;

    public GameObject lettuceParent;
    public GameObject appleParent;
    public List<GameObject> line;
    public int customersInLevel = 6;
    public int customersHelped = 0;
    public GameObject spawnArea;
    // Turn this into a list when using more than 1 type of customer
    public GameObject customerPrefab;

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
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Tutorial"))
        {
            // Spawn Customers
            if (trackWaves == customersHelped && trackWaves < customersInLevel)
            {
                if (trackWaves == 0)
                {
                    StartCoroutine(SpawnCustomerWave(1f));
                }
                else
                {
                    StartCoroutine(SpawnCustomerWave(2f));
                }
                trackWaves += 3;
            }
        }

        // Determine win conditions
        bool stocked = ShelvesStocked();
        bool helped = CustomersHelped();

        if (stocked && helped) 
        {
            StartCoroutine(WaitBeforeLoad());
        }
        else
        {
            clock += Time.deltaTime;
        }
        
        // Loss conditions (Take longer than 120 seconds)
        if (clock >= 160 && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Tutorial"))
        {
            SceneManager.LoadScene("Loss Screen");
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
        if (customersHelped >= customersInLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SpawnCustomerWave(float initialWait)
    {
        yield return new WaitForSeconds(initialWait);
        Instantiate(customerPrefab, spawnArea.transform);
        yield return new WaitForSeconds(1.5f);
        Instantiate(customerPrefab, spawnArea.transform);
        yield return new WaitForSeconds(1.5f);
        Instantiate(customerPrefab, spawnArea.transform);
    }

    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt("Level1Clock", (int)Math.Round(clock));
        SceneManager.LoadScene("Win Screen");
    }
}
