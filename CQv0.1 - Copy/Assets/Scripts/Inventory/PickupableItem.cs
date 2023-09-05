using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour {

    public ItemBase InventoryItem;
    bool playerTouchingItem = false;
    PlayerInventory playerInventory;
    GameObject player;
    bool isInteract;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        isInteract = player.GetComponent<PlayerController>().isInteracting;
        if (playerTouchingItem && isInteract)
        {
            Pickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory = other.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            //Debug.Log("Player touching object");
            playerTouchingItem = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Player  not touching object");
        playerTouchingItem = false;
    }

    private void Pickup()
    {
            playerInventory.AddItem(InventoryItem);
            //player.ReceiveItem(Instantiate(InventoryItem));
            Destroy(gameObject);
    }
}
