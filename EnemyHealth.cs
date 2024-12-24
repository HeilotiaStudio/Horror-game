using UnityEngine;
using UnityEngine.UI; // Needed for Slider

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider; // Reference to the slider
    public Animator enemyAnimator;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();  // Initialize the slider value
    }

   public void TakeDamage(int damage)
{
    currentHealth -= damage;
    Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

    if (currentHealth <= 0)
    {
        currentHealth = 0;
        UpdateHealthBar(); // Update the health bar before dying
        Die();
    }
    else
    {
        UpdateHealthBar();
    }
}

    void Die()
    {
        // Handle death logic (e.g., play death animation, destroy object)
        Debug.Log("Enemy has died.");
        // Optional: Play death animation
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }
    }



public void UpdateHealthBar()
{
    if (healthSlider != null)
    {
        if (currentHealth <= 0)
        {
            healthSlider.value = 0f; // Force slider to 0
        }
        else
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
        Debug.Log($"Health bar updated: {healthSlider.value}");
    }
    else
    {
        Debug.LogWarning("Health slider not assigned!");
    }
}
}
