using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider on the canvas
    public int maxHealth = 100; // Max health
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        print("Player died.");
        // Add death logic here (e.g., respawn, game over)
    }
}
