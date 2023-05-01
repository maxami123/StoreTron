using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Editable Player Variables")]
    public float playerSpeed = 1f;
    public float dashFactor = 2f;
    // How long we want the dash to last in seconds.
    public float maxDuration = 0.25f;
    // This is a very basic way to handle turning since we don't have enough that I'm willing to use an animation controller
    public float maxCooldown = 5.0f;
    public List<Sprite> sprites;

    [Header("Referenced Player Variables")]
    public float storedHorizontal;
    public float storedVertical;
    public bool inMenu;
    

    // Private variables
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private bool isDashing = false;
    private float dashLeft;
    private float horizontal;
    private float vertical;

    // Cooldown Variables
    public float dashCooldownTimer;
    public bool canDash = true;



    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = PlayerPrefs.GetFloat("DashCooldown");
        if (inMenu) { return; }
        // Get components off of object
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashLeft = maxDuration;
        dashCooldownTimer = maxCooldown;
        inMenu = false;
    }

    void Update()
    {
        // Better to get player input in update so I'm doing all of this in here
        if (inMenu) { return; }
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
        if (!canDash)
        {
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
                dashCooldownTimer = maxCooldown;
            }
            dashCooldownTimer -= (Time.deltaTime);
        }
        else 
        {
            if (dashLeft < maxDuration && !isDashing)
            {
                dashLeft += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) // Key must be held down to get maximum effect of dash
            {
                isDashing = true;
                canDash = false;
            }
        }
        
    }

    // Depending on input we are going to use a different sprite for the sprite renderer
    void ChooseSprite()
    {
        if (vertical < 0f)
        {
            spriteRenderer.sprite = sprites[0];
            storedVertical = -1f;
            storedHorizontal = 0f;
        }
        else if (vertical > 0f)
        {
            spriteRenderer.sprite = sprites[1];
            storedVertical = 1f;
            storedHorizontal = 0f;
        }
        else if (horizontal < 0f)
        {
            spriteRenderer.sprite = sprites[2];
            storedHorizontal = -1f;
            storedVertical = 0f;
        }
        else if (horizontal > 0f)
        {
            spriteRenderer.sprite = sprites[3];
            storedHorizontal = 1f;
            storedVertical= 0f;
        }
    }
}
