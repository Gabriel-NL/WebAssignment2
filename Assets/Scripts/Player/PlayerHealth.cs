using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health
    private int currentHealth;   // Player's current health

    public float invincibilityTime = 1f; // Time after getting hit before taking damage again
    private bool isInvincible = false;   // Prevents taking damage too frequently

    void Start()
    {
        currentHealth = maxHealth; // Set starting health
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        if (isInvincible) return;  // Ignore damage if invincible

        currentHealth -= damage;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCooldown());
        }
    }

    IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Player healed! Current health: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the scene
    }

    internal void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}
