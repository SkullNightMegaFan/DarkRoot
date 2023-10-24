using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

// This script takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public static event Action OnGunAttack;
    public MouseBehaviour mouseBehaviour;
    public GameObject rotationAnchor;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;  //distance from object to check for collision ? how far the ray cast goes
    public float swordCollisionOffset = 0.06f;  //distance from object to check for collision ? how far the ray cast goes
    public ContactFilter2D movementFilter;  //determines what can collide ?
    public SwordAttack swordAttack;
    public GunAttack gunAttack;



    Vector2 movementInput;
    Vector2 direction;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();  //create empty list that will store the ray cast collisions
    bool canMove = true;
    public bool isInteracting = false;
    
    //dodgeroll variables.
    float dodgeCoolDownTime = 1.4f;
    public float dodgeDuration = 0.5f;
    public float dodgeForce = 10.0f;
    private bool canDodge = true;

    //might not need canShoot bool but you never know.  
    private bool canShoot = true;
    private float reloadTime = 1.0f;
    private int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    private float fireRate = .5f;
    private bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ROTATION SCRIPT
        Vector3 rotationAnchorPosition = rotationAnchor.transform.position;
        Vector3 mousePosition = mouseBehaviour.mousePosition;
        direction = mousePosition - rotationAnchorPosition;  // find vector toward mouse

        float angle = Vector2.SignedAngle(Vector2.down, direction);  // find angle between start position and mouse vector
        transform.eulerAngles = new Vector3(0, 0, angle);   // set the object’s Z rotation to the angle value


        //if movement input is not 0, try to move
        if (canMove) // check if movement has been turned off
        {

            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput); // try to move in the direction the player inputs

                if (!success)  // if movement fails, try to move either only on X or only on Y (allows player to slide around obstacles)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            //Set direction of sprite to movement direction

            if (movementInput.y < 0) //moving down
            {
                animator.SetBool("isMovingDown", true); animator.SetBool("isMovingUp", false); animator.SetBool("isMovingLeft", false); animator.SetBool("isMovingRight", false);
            }
            else if (movementInput.y > 0) //moving up
            {
                animator.SetBool("isMovingUp", true); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingLeft", false); animator.SetBool("isMovingRight", false);
            }
            else if (movementInput.x < 0) //moving left
            {
                animator.SetBool("isMovingLeft", true); animator.SetBool("isMovingUp", false); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingRight", false);
            } else if (movementInput.x > 0) //moving right
            {
                animator.SetBool("isMovingRight", true); animator.SetBool("isMovingUp", false); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingLeft", false);
            }
        }


       /* //shooting logic
        if (reloadTime ==  0.0f)
        {
            canShoot == true;
        }
        else
        {
            canShoot = false;

        }*/
       //if the player hits space while they are able to move they can perform a dodge roll
       if (canDodge && Input.GetKeyDown(KeyCode.Space))
        {
          
        DodgeRoll();
        }


        if (currentAmmo == 0)
        {
            canShoot = false;

        }
        if (isReloading)
        {
            // If reloading, count down the reload time.
            canShoot = false;
            reloadTime -= Time.deltaTime;
            print("Reloading");
          

            // If the reload time is complete, finish reloading.
            if (reloadTime <= 0)
            {
                FinishReloading();
              
            }
        }
        else
        {
            // Check for user input to reload the gun.
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        if (canShoot && Input.GetMouseButtonDown(0))
        {
            //OnFire();
            gunAttack.Attack();
            currentAmmo--;
            print("Shots Fired");
           // canShoot == false;

            //Trying to get the gun to fire when prerequisite conditions are fulfilled. 
        }
        else
        {
            //return;
        }
    }

    private bool TryMove(Vector2 direction) // Test if a collision prevents movement in either X or Y direction
    {
        if (direction != Vector2.zero)
        {
            //check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions (direction trying to move)
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0) // if there are no collisions, move
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //position + movement vector * move speed
                return true; // movement did occur
            }
            else
            {
                return false; // movement did not occur
            }
        }
        else
        { // can't move if there's no direction to move in
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

void Reload()
{
    // Check if the gun is already full.
    if (currentAmmo == maxAmmo)
    {
            // Gun is already full, no need to reload.
            Debug.Log("Ammo count is full");
        return;
    }

    // Start the reloading process.
    isReloading = true;
    reloadTime = 1.0f;  // Reset the reload time.

    // You can add visual and audio effects for reloading here.

    // Simulate the reload time.
    // For a real game, you'd play an animation or perform other actions.
    // For this example, we just wait for the reload time.
}

void FinishReloading()
{
    // Calculate how much ammo to add to reach the maximum capacity.
    int ammoToAdd = maxAmmo - currentAmmo;

    // Add the ammo and clamp it to the maximum capacity.
    currentAmmo += ammoToAdd;
    currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

    // Reset the reloading flag.
    isReloading = false;
        canShoot = true;
    }

void DodgeRoll()
    {
        //what the dodge roll actually does
        //first calculate what direction the player is moving. 
        
        canDodge = false;
        rb.AddForce(movementInput * dodgeForce);
        spriteRenderer.color = Color.blue;
        Debug.Log("A dodgeroll was performed");

        
        StartCoroutine(StartDodgeCooldown());
        //play animation for dodge roll
        // animator.SetBool("dodge", true);
    }

    IEnumerator StartDodgeCooldown() 
    {
        yield return new WaitForSeconds(dodgeCoolDownTime);

        //Reset status to pre roll, this includes invinciblity and color change
        Debug.Log("Dodgeroll is finished");
        canDodge = true;
        spriteRenderer.color = Color.white;

    }
 
        void OnMelee()
    {
        int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions (direction trying to move)
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + swordCollisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0) // if there are no collisions, move
        {
            animator.SetBool("swordAttack", true);
            swordAttack.Attack();
        }
        else
        {
            animator.SetBool("swordAttack", true);
            swordAttack.FailedAttack();
        }


    }

    void OnInteract()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            StartCoroutine(PlayerInteractEnd());
        }
        else
        {
            isInteracting = false;
        }

    }
    IEnumerator PlayerInteractEnd()
    {
        yield return new WaitForSeconds(0.1f);
        isInteracting = false;
    }

    public void LockMovement()  //prevents the player from moving
    {
        canMove = false;
    }
    public void UnlockMovement()  //allows the player to move again
    {
        canMove = true;
    }
}
    