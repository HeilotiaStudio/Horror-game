using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyTag;             // Unique tag for this key (e.g., "KeyA", "KeyB")
    public Transform player;          // Reference to the player
    public float pickupDistance = 2.0f; // Distance to pick up the key

    private PlayerInventory playerInventory;

    void Start()
    {
        // Find the player's inventory script
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        // If the player is close enough and presses 'E'
        if (dist < pickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            // Add the key to the player's inventory and destroy the key object
            playerInventory.PickUpKey(keyTag);
            Destroy(gameObject); // Remove the key from the scene
        }
    }
}
