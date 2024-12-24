using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponName;         // Unique name for the weapon (e.g., "Pistol", "Rifle")
    public int ammo = 30;             // Starting ammo for this weapon
    public int damage = 10;           // Damage for this weapon
    public Transform player;          // Reference to the player
    public float pickupDistance = 2.0f; // Distance to pick up the weapon

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
            // Add the weapon or ammo to the player's inventory
            playerShooting.AddWeapon(weaponName, ammo, damage);
            Destroy(gameObject); // Remove the weapon or ammo object from the scene
        }
    }
}
