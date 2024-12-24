using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10; // Amount of damage dealt to the player
    public float attackCooldown = 1.0f; // Time between attacks

    private float nextAttackTime = 0f; // Tracks when the zombie can attack again

    void OnTriggerEnter(Collider other)
    {
        if (Time.time >= nextAttackTime)
        {
            if (other.CompareTag("Player")) // Check if the collided object is the player
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); // Apply damage to the player
                    Debug.Log("Zombie hit the player, dealt " + damage + " damage.");
                }

                nextAttackTime = Time.time + attackCooldown; // Set the next attack time
            }
        }
    }
}