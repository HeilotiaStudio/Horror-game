using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera playerCamera; // Camera to raycast from
    public float range = 100.0f; // Max range of the weapon
    public Transform weaponSlot; // Location where weapons appear visually (optional)

    private Dictionary<string, WeaponData> weaponInventory = new Dictionary<string, WeaponData>(); // Inventory with ammo and damage
    private string currentWeapon = null; // Name of the currently equipped weapon

    void Update()
    {
        // Equip weapon with 1, 2, or 3 keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon("Pistol");
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon("Shotgun");
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon("Rifle");

        // Shoot with left mouse button
        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            if (weaponInventory[currentWeapon].ammo > 0)
            {
                Shoot();
            }
            else
            {
                print("Out of ammo for " + currentWeapon);
            }
        }
    }

    void EquipWeapon(string weaponName)
    {
        if (weaponInventory.ContainsKey(weaponName))
        {
            currentWeapon = weaponName;
            print("Equipped " + weaponName);

            // Optional: Visual representation of weapon in hand
            if (weaponSlot != null)
            {
                // Add logic to show weapon model in weaponSlot
            }
        }
        else
        {
            print("You don't have " + weaponName);
        }
    }

    public void AddWeapon(string weaponName, int ammo, int damage)
    {
        if (weaponInventory.ContainsKey(weaponName))
        {
            weaponInventory[weaponName].ammo += ammo; // Add ammo if weapon is already in inventory
            print("Added " + ammo + " ammo to " + weaponName);
        }
        else
        {
            weaponInventory.Add(weaponName, new WeaponData(ammo, damage)); // Add new weapon with damage
            print("Picked up " + weaponName + " with " + ammo + " ammo.");
        }
    }

    void Shoot()
{
    print("Shooting " + currentWeapon);
    weaponInventory[currentWeapon].ammo--;

    RaycastHit hit;
    if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
    {
        print("Hit " + hit.collider.name);

        // Check if the hit object has the "Enemy" tag
        if (hit.collider.CompareTag("Enemy"))
        {
            EnemyHealth targetHealth = hit.collider.GetComponent<EnemyHealth>();
            if (targetHealth != null)
            {
                int damage = weaponInventory[currentWeapon].damage; // Get weapon damage
                targetHealth.TakeDamage(damage); // Deal damage
            }
        }
    }
}

    // Method for picking up ammo
    public void AddAmmo(string weaponName, int ammo)
    {
        if (weaponInventory.ContainsKey(weaponName))
        {
            weaponInventory[weaponName].ammo += ammo; // Add ammo to existing weapon
            print("Picked up " + ammo + " ammo for " + weaponName);
        }
        else
        {
            print("Cannot add ammo. You don't have the weapon: " + weaponName);
        }
    }
}

// Helper class to store ammo and damage for weapons
[System.Serializable]
public class WeaponData
{
    public int ammo;
    public int damage;

    public WeaponData(int ammo, int damage)
    {
        this.ammo = ammo;
        this.damage = damage;
    }
}

