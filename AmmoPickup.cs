using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public string weaponName; // Name of the weapon this ammo belongs to (e.g., "Pistol")
    public int ammoAmount = 10; // Amount of ammo to give
    public Transform player; // Reference to the player
    public float pickupDistance = 2.0f; // Distance to pick up the ammo

    private PlayerShooting playerShooting;

    void Start()
    {
        playerShooting = player.GetComponent<PlayerShooting>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < pickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            playerShooting.AddAmmo(weaponName, ammoAmount);
            Destroy(gameObject); // Remove the ammo pickup from the scene
        }
    }
}

