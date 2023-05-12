using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialController : MonoBehaviour
{
    public DialogueRenderer[] dialogue;
    public GameObject[] instructions;
    [SerializeField] private int index;
    public GameObject background;
    public GameObject inventor;
    public GameObject text;
    public List<bool> completed;
    public Canvas website;
    public NavMeshAI ai;

    // Movement Instructions
    private bool left;
    private bool right;
    private bool up;
    private bool down;


    // Order Item Instructions
    public GameObject inventoryUI;
    public GameObject webpage;
    public GameObject orderItemArrow;


    // Pick Up Instructions
    public GameObject[] emptyShelves;
    public GameObject pickupArrow;

    // Customer Arrives
    public GameObject customerAI;
    

    // Customer Gets Item
    public Transform lineArea;

    // Customer Checkout
    public GameObject checkoutArrow;
    // Tutorial Finish
    public GameObject customer;

    // Start is called before the first frame update
    void Start()
    {
        website.sortingOrder = 0;
        orderItemArrow.SetActive(false);
        pickupArrow.SetActive(false);
        checkoutArrow.SetActive(false);
        foreach (DialogueRenderer dr in dialogue)
            dr.gameObject.SetActive(false);
        foreach (GameObject inst in instructions)
            inst.SetActive(false);
        for (int i = 0; i < dialogue.Length; i++)
            completed.Add(false);
        index = 0;
        dialogue[index].gameObject.SetActive(true);
        instructions[index].SetActive(false);
        inventoryUI.SetActive(false);
        dialogue[index].StartDialogue(background, inventor, text);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogue[index].gameObject.activeSelf)
        {
            if (!completed[index])
            {
                Debug.Log("Part " + index);
                instructions[index].SetActive(true);
                switch (index){
                    case 0:
                        MovementInstructions();
                        break;
                    case 1:
                        OrderItemsInstructions();
                        break;
                    case 2:
                        PlaceHolderInstructions();
                        break;
                    case 3:
                        PickUpInstructions();
                        break;
                    case 4:
                        BoostInstructions();
                        break;
                    case 5:
                        CustomerArrives();
                        break;
                    case 6:
                        CustomerGetsItem();
                        break;
                    case 7:
                        CustomerCheckout();
                        break;
                    case 8:
                        TutorialFinish();
                        break;

                }
            }
            

        }

    }

    private void NextPart()
    {
        completed[index] = true;
        instructions[index].SetActive(false);
        index++;
        dialogue[index].gameObject.SetActive(true);
        dialogue[index].StartDialogue(background, inventor, text);
        //Debug.Log(index);

    }
    private void MovementInstructions() // Part 1
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            left = true;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            right = true;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            up = true;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            down = true;

        if (left && up && down && right)
        {
            //Activate the next part of the tutorial
            NextPart();

        }
    }

    private void OrderItemsInstructions() //Part 2
    {
        if (!orderItemArrow.activeSelf)
            orderItemArrow.SetActive(true);
        if (!inventoryUI.activeSelf)
            inventoryUI.SetActive(true);

        if (webpage.activeSelf)
        {
            orderItemArrow.SetActive(false);
            NextPart();
            Debug.Log("OrderItemsInstructions");
        }
    }
    private void PlaceHolderInstructions() // Part 3
    {
        if (!webpage.activeSelf)
        {
            NextPart();
            Debug.Log("PlaceHolderInstructions Completed");
        }
            
        
    }

    private void PickUpInstructions() // Part 4
    {
        website.sortingOrder = 4;
        if (!pickupArrow.activeSelf)
            pickupArrow.SetActive(true);

        int count = 0;
        foreach (GameObject shelf in emptyShelves)
        {
            if (!shelf.activeSelf)
                count++;
        }
        if (count >= emptyShelves.Length/2)
        {
            pickupArrow.SetActive(false);
            NextPart();
            Debug.Log("PickUpInstructions Completed");
        }
    }

    private void BoostInstructions() //Part 5
    {
        int count = 0;
        foreach (GameObject shelf in emptyShelves)
        {
            if (!shelf.activeSelf)
                count++;
            if (count >= emptyShelves.Length)
            {
                NextPart();
               Debug.Log("BoostInstructions Completed");
            }
                

        }
    }

    private void CustomerArrives() //Part 6
    {
        if (!customerAI.activeSelf && !completed[5])
        {
            completed[5] = true;
            customerAI.SetActive(true);
            Debug.Log("CustomerArrives Completed");
            StartCoroutine(DelayedNextPart());
        }
    }   

    IEnumerator DelayedNextPart()
    {
        yield return new WaitForSeconds(1);

        
        NextPart();
    }

    private void CustomerGetsItem() // Part 7
    {
        if (!customerAI.GetComponentInChildren<NavMeshAI>().enabled)
            customerAI.GetComponentInChildren<NavMeshAI>().enabled = true;
        if (ai.inLine)
        {
            NextPart();
            Debug.Log("CustomerGetsItem Completed");   
        }
    }

    private void CustomerCheckout()
    {
        if (!checkoutArrow.activeSelf)
            checkoutArrow.SetActive(true);
        Debug.Log("Checking for Customer leaving");
        if (!customer.activeSelf)
        {
            Debug.Log("CustomerCheckout Completed");
            checkoutArrow.SetActive(false);
            NextPart();
        }
             
    }

    private void TutorialFinish()
    {
        SceneManager.LoadScene("Upgrades");
    }


    



}
