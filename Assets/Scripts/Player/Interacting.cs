using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    private Movement movement;
    private float horizontalDir;
    private float verticalDir;

    // Start is called before the first frame update
    void Start()
    {
        // Figure out which direction the character is facing (I'm going to have it focus on the 4 cardinal directions until we get diagonal animations for storetron)
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDir = movement.storedHorizontal;
        verticalDir = movement.storedVertical;
        Interact();
    }

    // Interact with an object if possible
    void Interact()
    {
        // Check to see if the interact button was pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Figure out what is in front of the player
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up * verticalDir * 2 + transform.right * horizontalDir * 2, 0.6f);
            // Check to see if there is something collidable in front of the player
            if (hitInfo.collider != null)
            {
                // Check to see if the object in front of the player is interactable
                if (hitInfo.transform.CompareTag("Interactable"))
                {
                    Debug.Log(hitInfo.transform.name);
                    // Run the interact code on the IInteractable script
                    hitInfo.transform.GetComponent<IInteractable>().Interact();
                }
            }
            
        }
    }
}
