using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{
    public AudioClip coinSound;

    private void Start()
    {
    }

    void Update()
    {
        transform.Rotate(0, 0, 70 * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6: //  Player layer: give loot
                if (other.tag == "Player")   
                {
                    PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

                    if (playerInventory != null)
                    {
                        PlayCoinPickup();
                        Destroy(gameObject);
                        other.gameObject.GetComponent<PlayerInventory>().AddCarrotSeeds(1);
                    }

                }
                break;
            default:
                break;
        }

    }
    public void PlayCoinPickup()
    {
        GameObject sfxManager = GameObject.Find("SFXManager");
        AudioSource audio = sfxManager.GetComponent<AudioSource>();

        audio.PlayOneShot(coinSound, 0.3f);

    }

}
