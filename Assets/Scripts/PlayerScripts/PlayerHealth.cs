using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // UI Elements
    public TMP_Text healthText;

    private void Start()
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

    private void Die()
    {
        // Notify the GameManager about the player's death
        GameManager.instance.HandlePlayerDeath();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }
}