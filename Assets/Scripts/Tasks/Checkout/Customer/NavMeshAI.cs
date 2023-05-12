using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NavMeshAI : MonoBehaviour
{

    public List<GameObject> parentReferenceObjects;
    public GameObject destinationParent;
    public GameObject taskHandler;
    public bool inLine = false;
    public bool leavingAngry = false;

    private Transform angrySymbol;
    private float timer = 0;
    private bool shown = false;
    private NavMeshAgent agent;
    private Transform target;
    private Vector3 targetPosition;
    private List<Transform> references = new List<Transform>();
    private bool grabbedProduct;
    private Animator customerAnimator;
    private int lineIndex;
    private List<Transform> destinations;

    private AudioSource spawnNotif;
    // Called on instance creation
    void Awake()
    {
        spawnNotif = GetComponent<AudioSource>();
        // Play sound when spawned
        spawnNotif.Play();
        // Initialize all public variables for prefab instance
        parentReferenceObjects = GameObject.FindGameObjectsWithTag("Shelves").ToList();
        destinationParent = GameObject.FindGameObjectWithTag("Destinations");
        taskHandler = GameObject.FindGameObjectWithTag("GameController");

        // Initialize all the destination order
        destinations = destinationParent.GetComponentsInChildren<Transform>().ToList();
        destinations.Remove(destinationParent.transform);

        // Convert parent references into a list of potential targetPosition locations
        foreach (var parent in parentReferenceObjects)
        {
            var children = parent.GetComponentsInChildren<Transform>(true).ToList();
            children.Remove(parent.transform);
            foreach (var trans in children)
            {
                if (trans.GetComponent<SpriteRenderer>() == null)
                {
                    references.Add(trans);
                }
            }
        }
        agent = GetComponent<NavMeshAgent>();
        customerAnimator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    // Start is called before the first frame update
    void Start()
    {   
        
        // Get initial location to go to
        var choice = Random.Range(0, references.Count - 1);
        target = references[choice].transform;
        targetPosition = target.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y-0.5f, targetPosition.z);
        agent.SetDestination(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Control agent animation
        if (agent.hasPath)
        {
            var directionHandler = agent.destination - transform.position;
            directionHandler = directionHandler.normalized;
            customerAnimator.SetFloat("X", directionHandler.x);
            customerAnimator.SetFloat("Y", directionHandler.y);
        }

        if (!grabbedProduct)
        {
            GrabProductLogic();
        }
        else
        {
            if (SceneManager.GetSceneByName("Tutorial") != SceneManager.GetActiveScene())
            {
                if (timer >= 5f && !shown)
                {
                    StartCoroutine(ShowAngry());
                }
                if (timer >= 10f)
                {
                    StartCoroutine(EndLevel());
                    agent.SetDestination(destinations[1].position);
                    if (Vector2.Distance(transform.position, destinations[1].position) < 0.25f)
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<BoxCollider2D>().enabled = false;
                    }
                }

            }
            // If not inline then we should move the agent to the line location
            if (!inLine)
            {
                // Once we're there we're going to set inline to true
                if (Vector2.Distance(transform.position, destinations[0].position) > 0.25f)
                {
                    lineIndex = taskHandler.GetComponent<TaskHandler>().line.IndexOf(transform.gameObject);
                    var finalLocation = new Vector3(destinations[0].position.x, (float)(destinations[0].position.y + (2.5 * lineIndex)), destinations[0].position.z);
                    agent.SetDestination(finalLocation);
                }
                else
                {
                    inLine = true;
                }
            }
            // Once inline is true this else would likely hold AI logic for being helped then exiting the store
            else
            {
                if (!taskHandler.GetComponent<TaskHandler>().line.Contains(transform.gameObject)) 
                {
                    if (transform.gameObject.activeSelf)
                    {
                        agent.SetDestination(destinations[1].position);
                    }
                    if (Vector2.Distance(transform.position, destinations[1].position) < 0.25f)
                    {
                        gameObject.SetActive(false);
                    }
                }
                else 
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    void GrabProductLogic()
    {
        // If another customer grabs it then reroute to a new shelf
        if (target.gameObject.activeSelf)
        {
            references.Remove(target);
            var choice = Random.Range(0, references.Count - 1);
            target = references[choice].transform;
            targetPosition = target.position;
            targetPosition = new Vector3(targetPosition.x, targetPosition.y-0.5f, targetPosition.z);
            agent.SetDestination(targetPosition);
        }

        // Wait for a second before grabbing produce and then moving to the register
        if (Vector2.Distance(transform.position, targetPosition) < 1f)
        {
            StartCoroutine(GrabProduct());
        }
    }

    IEnumerator GrabProduct()
    {
        yield return new WaitForSeconds(1f);
        if (!target.gameObject.activeSelf)
        {
            grabbedProduct = true;
            target.gameObject.SetActive(true);
            taskHandler.GetComponent<TaskHandler>().line.Add(transform.gameObject);
        }
    }

    IEnumerator ShowAngry()
    {
        shown = true;
        angrySymbol = GetComponentsInChildren<Transform>(true)[1];
        angrySymbol.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        angrySymbol.gameObject.SetActive(false);
    }

    IEnumerator EndLevel()
    {
        leavingAngry = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Loss Screen");
    }
}
