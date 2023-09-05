using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public PlayerStatus playerStatus;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer
                break;
            case 7:
                //  Enemies layer: collide
                int damage = other.gameObject.GetComponent<Enemy>().damage;
                //playerStatus.PlayerTakeDamage(damage);
                this.transform.parent.gameObject.GetComponent<PlayerStatus>().PlayerTakeDamage(damage);

                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
        }

    }
}
