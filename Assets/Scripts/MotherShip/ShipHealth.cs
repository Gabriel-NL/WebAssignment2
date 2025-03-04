using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;

    // Update is called once per frame
    void Update()
    {
        // Check if health has reached zero
        if (health <= 0)
        {
            // Switch to scene 3
            SceneManager.LoadScene(3);
        }
    }

    // Call this method when the ship is hit
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;  // Reduce the health by damage amount
        if (health < 0) health = 0;  // Ensure health doesn't go below zero
    }

    // Example collision method to handle damage when the ship collides with an enemy
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy (you can tag or layer the enemy for better collision handling)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damage);
        }
    }
}
