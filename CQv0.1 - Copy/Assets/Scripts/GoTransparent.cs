using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTransparent : MonoBehaviour
{
    SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer
                sprite.color = new Color(1f, 1f, 1f, 0.5f);
                break;
            case 7:
                //  Enemies layer
                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
        }

    }
    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer
                sprite.color = new Color(1f, 1f, 1f, 1f);
                break;
            case 7:
                //  Enemies layer
                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
        }

    }

}
