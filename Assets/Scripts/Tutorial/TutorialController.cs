using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialController : MonoBehaviour
{
    public DialogueRenderer[] dialogue;
    public GameObject[] instructions;
    [SerializeField] private int index;
    public GameObject background;
    public GameObject inventor;
    public GameObject text;

    // Movement Instructions
    private bool left;
    private bool right;
    private bool up;
    private bool down;


    // Order Item Instructions
    public GameObject inventoryUI;
    public GameObject webpage;


    // Pick Up Instructions
    public GameObject[] emptyShelves;

    // Start is called before the first frame update
    void Start()
    {
        foreach (DialogueRenderer dr in dialogue)
            dr.gameObject.SetActive(false);
        foreach (GameObject inst in instructions)
            inst.SetActive(false);
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
            instructions[index].SetActive(true);
            if (index == 0)
                MovementInstructions();
            if (index == 1)
                OrderItemsInstructions();
            if (index == 2)
                PlaceHolderInstructions();
            if (index == 3)
                PickUpInstructions();

        }

    }

    private void NextPart()
    {
        instructions[index].SetActive(false);
        index++;
        dialogue[index].gameObject.SetActive(true);
        dialogue[index].StartDialogue(background, inventor, text);
        Debug.Log(index);

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

        if (!inventoryUI.activeSelf)
            inventoryUI.SetActive(true);

        if (webpage.activeSelf)
            NextPart();
    }
    private void PlaceHolderInstructions() // Part 3
    {
        if (!webpage.activeSelf)
            NextPart();
        
    }

    private void PickUpInstructions() // Part 4
    {
        int count = 0;
        foreach (GameObject shelf in emptyShelves)
        {
            if (!shelf.activeSelf)
                count++;
        }
        if (count >= 3)
            NextPart();
    }

    private void BoostIntructions() //Part 5
    {
        int count = 0;
        foreach (GameObject shelf in emptyShelves)
        {
            if (!shelf.activeSelf)
                count++;
            if (count >= 6)
                NextPart();

        }
    }










}
