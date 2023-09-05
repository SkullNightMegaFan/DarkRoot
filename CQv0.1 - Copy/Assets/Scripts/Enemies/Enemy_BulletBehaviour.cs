using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletBehaviour : BulletBehaviour
{

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer: collide and do damage
                Destroy(this.gameObject);
                PlayerStatus player = other.GetComponent<PlayerStatus>();  //grab the enemy script attached to the collided enemy 

                if (player != null)
                {
                    player.PlayerTakeDamage(damage);
                }
                break;
            case 7:
                //  Enemies layer: don't collide
                break;
            case 8:
                //  Projectiles layer: ignore for now
                Destroy(this.gameObject);
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }


}
