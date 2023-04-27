using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{

    public List<GameObject> parentReferenceObjects;
    public GameObject lineLocation;

    private NavMeshAgent agent;
    private Transform target;
    private Vector3 targetPosition;
    private List<Transform> references = new List<Transform>();
    private bool grabbedProduct;
    private bool inLine = false;
    private Animator customerAnimator;

    // Called on instance creation
    void Awake()
    {
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
        var choice = Random.Range(0, references.Count - 1);
        target = references[choice].transform;
        targetPosition = target.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y-0.5f, targetPosition.z);
        agent.SetDestination(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
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
            // If not inline then we should move the agent to the line location
            if (!inLine)
            {
                // Once we're there we're going to set inline to true
                if (Vector2.Distance(transform.position, lineLocation.transform.position) > 0.5f)
                {
                    agent.SetDestination(lineLocation.transform.position);
                }
                else
                {
                    inLine = true;
                }
            }
            // Once inline is true this else would likely hold AI logic for being helped then exiting the store
            else
            {

            }
        }
    }

    void GrabProductLogic()
    {
        // If another customer grabs it then reroute to a new shelf
        if (target.gameObject.activeSelf)
        {
            references.Remove(target);
            Debug.Log("Rerouting");
            var choice = Random.Range(0, references.Count - 1);
            target = references[choice].transform;
            targetPosition = target.position;
            targetPosition = new Vector3(targetPosition.x, targetPosition.y-0.5f, targetPosition.z);
            agent.SetDestination(targetPosition);
        }

        // Wait for a second before grabbing produce and then moving to the register
        if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
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
        }
    }
}
