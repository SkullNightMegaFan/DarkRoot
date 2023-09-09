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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // ROTATION SCRIPT
        Vector3 rotationAnchorPosition = rotationAnchor.transform.position;
        Vector3 mousePosition = mouseBehaviour.mousePosition;
        direction = mousePosition - rotationAnchorPosition;  // find vector toward mouse

        float angle = Vector2.SignedAngle(Vector2.down, direction);  // find angle between start position and mouse vector
        transform.eulerAngles = new Vector3(0, 0, angle);   // set the object�s Z rotation to the angle value


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
            } else if (movementInput.y > 0) //moving up
            {
                animator.SetBool("isMovingUp", true); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingLeft", false); animator.SetBool("isMovingRight", false);
            } else if (movementInput.x < 0) //moving left
            {
                animator.SetBool("isMovingLeft", true); animator.SetBool("isMovingUp", false); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingRight", false);
            } else if (movementInput.x > 0) //moving right
            {
                animator.SetBool("isMovingRight", true); animator.SetBool("isMovingUp", false); animator.SetBool("isMovingDown", false); animator.SetBool("isMovingLeft", false);
            }
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

    void OnFire()
    {
        gunAttack.Attack();
        OnGunAttack?.Invoke();
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