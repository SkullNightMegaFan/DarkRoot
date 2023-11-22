using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float bulletMoveSpeed = 4;
    public int damage = 5;

    public Vector2 velocity;
    public Vector3 direction;
    float timeUntilDestroyed = 10.0f;


    void Update()
    {
        // Move the bullet in the direction of our velocity.
        direction = ((Vector3)velocity).normalized * bulletMoveSpeed * Time.deltaTime;
        transform.position += direction;
        Rotate();

        //destroy if still around after a while
        DestroyAfterDelay();
    }
    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
        //bullets die when touching an obstacle.
        //eventually need to make an exception for a bouncy weapon or something
        case 3:
        Destroy(this.gameObject);
        break;
        //i want the bullet to collide but not to be destroyed
        case 6:
            //  Player layer: don't collide
            break;
        case 7:
                //  Enemies layer: collide and do damage
                Destroy(this.gameObject);
                Enemy enemy = other.GetComponent<Enemy>();  //grab the enemy script attached to the collided enemy 

                if (enemy != null)
                {
                    enemy.EnemyTakeDamage(damage);
                }
                break;
        // case 8:
        //         //  Projectiles layer: ignore for now
        //         Destroy(this.gameObject);
        //         break;
        case 15:
                Destroy(this.gameObject);
                break;

            default:
                
                break;
        }

    }

    void DestroyAfterDelay()
    {
        Object.Destroy(gameObject, timeUntilDestroyed);
    }
    void Rotate()
    {
        float angle = Vector2.SignedAngle(Vector2.down, direction);  // find angle between start position and mouse vector
        transform.eulerAngles = new Vector3(0, 0, angle);   // set the object�s Z rotation to the angle value
    }
}
