using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float playerSpeed = 1f;
    public float dashFactor = 2f;
    // How long we want the dash to last in seconds.
    static public float maxDuration = 0.25f;
    // This is a very basic way to handle turning since we don't have enough that I'm willing to use an animation controller
    public List<Sprite> sprites;

    // Private variables
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float horizontal;
    private float vertical;
    private bool isDashing = false;
    private float dashLeft = maxDuration;



    // Start is called before the first frame update
    void Start()
    {
        // Get components off of object
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Better to get player input in update so I'm doing all of this in here
        GetMovement();
        ChooseSprite();
        Dash();
    }
    void FixedUpdate()
    {
        // Change velocity depending on whether or not the player is dashing
        if (isDashing)
        {
            body.velocity = new Vector2(horizontal * playerSpeed * dashFactor, vertical * playerSpeed * dashFactor);
            dashLeft -= Time.fixedDeltaTime;
            if (dashLeft <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            body.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
        }
    }

    // Gets the player input for the character
    void GetMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void Dash()
    {
        if (dashLeft < maxDuration && !isDashing)
        {
            dashLeft += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }
    }

    // Depending on input we are going to use a different sprite for the sprite renderer
    void ChooseSprite()
    {
        if (vertical < 0f)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (vertical > 0f)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (horizontal < 0f)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (horizontal > 0f)
        {
            spriteRenderer.sprite = sprites[3];
        }
    }
}
