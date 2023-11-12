using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

// This script takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public static event Action OnGunAttack;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.04f;  //distance from object to check for collision ? how far the ray cast goes
    public float swordCollisionOffset = 0.06f;  //distance from object to check for collision ? how far the ray cast goes
    public ContactFilter2D movementFilter;  //determines what can collide ?
    public SwordAttack swordAttack;
    public GunAttack gunAttack;



    Vector2 movementInput;
    Vector2 direction;
    SpriteRenderer spriteRenderer;
    ParticleSystem burrowParticleSystem;
    public Rigidbody2D rb;
    Animator animator;
    PlayerInventory playerInventory;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();  //create empty list that will store the ray cast collisions
    bool canMove = true;
    public bool isInteracting = false;
    
    //dodgeroll variables.
    float dodgeCoolDownTime = 1.4f;
    public float dodgeDuration = 5.0f;
    public float dodgeForce = 10.0f;
    private bool canDodge = true;
   

    //might not need canShoot bool but you never know.  
    private bool canShoot = true;
    private float reloadTime = 1.0f;
    public int maxAmmo = 6;
    public int currentAmmo;
    private bool isReloading = false;
    private float fireRate = .5f;
    public bool isInvincible = false;

    //burrowVariables
    public float burrowMeter;
    public float maxBurrowMeter = 3.0f;
    public bool isBurrowing;
    public Vector2 introBurrowPoint;
    public Vector2 exitBurrowPoint;
    public GameObject IntroBurrow;
    public GameObject ExitBurrow;
    //ideally this should be a list that the player's code reads from
    //and decides which dirt texture/effect to spawn. 
    private GameObject burrowParticle;
    //debug not going to set canBurrow in full game
    
   // private bool isburrowMeterEmpty;
    //private bool canBurrow = isburrowMeterEmpty && ;
    private bool canBurrow = true;
    private bool canBurrowExit; // = false;

    //need to create a state to track when the player is exiting a burrow.
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInventory = GetComponent<PlayerInventory>();
        //burrowParticle = GetComponent<ParticleSystem>();
        currentAmmo = maxAmmo;
        burrowMeter = maxBurrowMeter;
        ///
        burrowParticleSystem = this.GetComponentInChildren<ParticleSystem>();

    }
     void Update()
    {
       

        //this line of code prevents burrowmeter from surpassing maxBurrowMeter
        burrowMeter = Mathf.Clamp(burrowMeter, 0, maxBurrowMeter);
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
                animator.SetBool("isMovingDown", true);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingLeft", false); 
                animator.SetBool("isMovingRight", false);
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

       //if the player hits space while they are able to move they can perform a dodge roll
        if (canDodge && Input.GetKeyDown(KeyCode.Space))
        {  
            DodgeRoll();
        }



        //if player can burrow and is inputting the burrow button, they will be able to burrow. 
        if (canBurrow && Input.GetButton("Burrow"))
        {
                    //isBurrowing = true;

            canBurrow = false;
            
            Burrow();
            // if (isBurrowing)
            // {
            //     BurrowExit();
            // }
        }
        if (Input.GetKeyDown("e"))
        {
            // Debug.Log("Teleportation activated");
            // rb.position = introBurrowPoint;
            OnInteract();
        }

        // if (canBurrowExit && Input.GetButton("Burrow") && isBurrowing)
        // {
        //     canBurrowExit = false;
        //     BurrowExit();
        // }


         if (isBurrowing)
         {

            Debug.Log("Currently Burrowing");
              //player is invincible while burrowing.
            spriteRenderer.color = Color.clear;
            //!burrowParticleSystem.enabled = burrowParticleSystem.enabled;
            // burrowParticleSystem.Play();
            ///////////////////////
            //!burrowParticle.enabled = burrowParticle.enabled;
            //burrowParticle.SetActive(true);
            
            //later we need to change this so the player creates a dig effect based on 
            //on the floor beneath them. So if the player is in a snowy area, a snow type
            //burrow effect should appear, for now we can get away with just spawning in dirt
            //Instantiate()

            burrowMeter -= 1 * Time.deltaTime;
            canBurrowExit = true;
            
            //Debug.Log(burrowMeter);
              if (burrowMeter > 0)
             {
           // isburrowMeterEmpty = false;
            }
            else 
             {
            if (isBurrowing)
            {
                BurrowExit();
            }
            //isBurrowing = false; 
           // isburrowMeterEmpty = true;
             }
         }
         else 
         {
            //while player is not burrowing, they recharge their burrowMeter as time passes
            burrowMeter += 1 * Time.deltaTime;
            //burrowParticle.enabled = !burrowParticle.enabled;
           // burrowParticle.SetActive(false);


         }

         //if the burrowMeter is more than zero, the player will be able to burrow
      




        if (currentAmmo <= 0)
        {
            canShoot = false;

        }
        else if (currentAmmo > 0)
        {
            canShoot = true;
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

        //if the player presses the shoot button and can shoot
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
    /////////////
    ////////////
///ned to create bool for TryBurrow. needs to be similar but have a different set of obstacles that the player can and cannot pass through
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

    else if (playerInventory.playerPistolAmmo == 0)
{
    Debug.Log("Out of ammo, young bunnaroo");
    return;
}    // Start the reloading process.
    isReloading = true;
    //look into making a variable rest into it's initialized value. 
    reloadTime = 1.0f;  // Reset the reload time.

    // You can add visual and audio effects for reloading here.

    // Simulate the reload time.
    // For a real game, you'd play an animation or perform other actions.
    // For this example, we just wait for the reload time.
}

void FinishReloading()
{
    // Calculate how much ammo to add to reach the maximum capacity.
    //need to add logic where player can reload with half ammo and max out at maxAmmo 
    //instead of being able to go over clip capacity.
    int ammoToAdd;
    if (playerInventory.playerPistolAmmo >= maxAmmo)
    {
        //might be double equal
        ammoToAdd = maxAmmo;
        playerInventory.playerPistolAmmo = playerInventory.playerPistolAmmo - maxAmmo;
        //Debug.Log()
    } 
    else 
    {
        ammoToAdd = playerInventory.playerPistolAmmo;
        playerInventory.playerPistolAmmo =  playerInventory.playerPistolAmmo - playerInventory.playerPistolAmmo;
    }

    // Add the ammo and clamp it to the maximum capacity.
    currentAmmo += ammoToAdd;
    currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
    if (currentAmmo > maxAmmo)
    {
        currentAmmo = maxAmmo;
    }
  

    // Reset the reloading flag.
    isReloading = false;
        canShoot = true;
    }


void DodgeRoll()
    {
        //what the dodge roll actually does
        //first calculate what direction the player is moving. 
        
        canDodge = false;
        isInvincible = true;
        rb.AddForce(movementInput * dodgeForce);
        spriteRenderer.color = Color.blue;
        Debug.Log("A dodgeroll was performed");

        //dodge duration
        StartCoroutine(DodgeRollDuration());
        //need length of time for dodgeroll

       // StartCoroutine(StartDodgeCooldown());

        //play animation for dodge roll
        // animator.SetBool("dodge", true);
    }

    IEnumerator DodgeRollDuration()
    {
        yield return new WaitForSeconds(dodgeDuration);
        StartCoroutine(StartDodgeCooldown());
    }
    IEnumerator StartDodgeCooldown() 
    {
        //The player is no longer invincible and is cooling down from dodging
        spriteRenderer.color = Color.yellow;
        isInvincible = false;
        yield return new WaitForSeconds(dodgeCoolDownTime);

        //Reset status to pre roll, this includes invinciblity and color change
        Debug.Log("Dodgeroll is finished");
        canDodge = true;
        spriteRenderer.color = Color.white;

    }
 
    void Burrow()
    {
        //starts the particle system when the player burrows
        burrowParticleSystem.Play();
        /*player becomes invincible, 
        //the player sprite becomes invisible
        the point is able 
        */
        //first the player must destroy the previous burrow's
        //Probably need to make an if statement if previous 
        //burrow's exist. But let's get it working first.
        Debug.Log("New Burrow is activated");

        //destroy previous pairs of burrows
        Destroy(IntroBurrow);
        Destroy(ExitBurrow);
        
             

        //creating the intro burrow point
        introBurrowPoint = rb.position;
        Instantiate(IntroBurrow, introBurrowPoint, Quaternion.identity);
        Debug.Log(introBurrowPoint);
        StartCoroutine(MiniWait());

        isBurrowing = true;
        //toggle burrow
       //StartCoroutine()



    }
    //when player exits the burrow
    void BurrowExit()
    {
        Debug.Log("Player has exited Burrow");
        //update later
        //stops the particle system
        burrowParticleSystem.Stop();
        exitBurrowPoint = rb.position;
        Instantiate(ExitBurrow, exitBurrowPoint,Quaternion.identity );
        spriteRenderer.color = Color.white;

           StartCoroutine(MiniWait());

        isBurrowing = false;
        
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

      IEnumerator MiniWait()
    {
        yield return new WaitForSeconds(0.1f);
        //isInteracting = false;
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
    