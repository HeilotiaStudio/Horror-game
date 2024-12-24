using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
     private List<string> acquiredKeys = new List<string>(); // List to store acquired key tags

    // Function to pick up a key
    public void PickUpKey(string keyTag)
    {
        if (!acquiredKeys.Contains(keyTag))
        {
            acquiredKeys.Add(keyTag);
            print("Picked up the key: " + keyTag);
        }
    }

    // Function to check if the player has a specific key
    public bool HasKey(string keyTag)
    {
        return acquiredKeys.Contains(keyTag);
    }

    // Function to reset all keys (e.g., on game reset)
    public void ResetKeys()
    {
        acquiredKeys.Clear();
        print("Keys have been reset.");
    } 
}
