using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueRenderer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
  
    public Movement movement;
    public Interacting interacting;

    private int index;
    private GameObject background;
    private GameObject inventor;
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; 
            }
        }
    }

    public void StartDialogue(GameObject bg, GameObject inv, GameObject txt)
    {
        background = bg;
        inventor = inv;
        text = txt;
        background.SetActive(true);
        inventor.SetActive(true);
        text.SetActive(true);
        movement.enabled = false;
        interacting.enabled = false;
        textComponent.text = string.Empty;  
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());  
        }
        else
        {
            movement.enabled = true;
            interacting.enabled = true;
            background.SetActive(false);
            inventor.SetActive(false);
            text.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    
}
