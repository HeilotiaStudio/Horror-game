using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
 public Animator openAndClose;
    public bool open;
    public Transform player;
    public float interactionDistance = 3.0f; // Distance at which the player can interact with the door
    public string requiredKeyTag;            // Tag of the required key object to open this door

    private PlayerInventory playerInventory; // Reference to the player's inventory

    void Start()
    {
        open = false;

        // Find the player inventory script on the player object
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        // If within interaction distance and player clicks
        if (dist < interactionDistance && Input.GetMouseButtonDown(0))
        {
            if (playerInventory != null && playerInventory.HasKey(requiredKeyTag))
            {
                // Player has the key, toggle the door
                ToggleDoor();
            }
            else
            {
                // Player doesn't have the correct key
                print("You need the " + requiredKeyTag + " to open this door.");
            }
        }
    }

    void ToggleDoor()
    {
        if (open)
        {
            StartCoroutine(Closing());
        }
        else
        {
            StartCoroutine(Opening());
        }
    }

    IEnumerator Opening()
    {
        print("You are opening the door");
        openAndClose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Closing()
    {
        print("You are closing the door");
        openAndClose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(0.5f);
    }
}
