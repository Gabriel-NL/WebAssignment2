using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public float lifetime = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Projectile missing Rigidbody! Add one in the Inspector.");
        }

        rb.linearVelocity = transform.forward * speed; // Move forward

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MotherShip") || other.CompareTag("Player"))
        {
            if (other.gameObject == transform.root.gameObject) return; // Avoid self-hit

            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null) playerHealth.TakeDamage(damage);
            }
            else if (other.CompareTag("MotherShip"))
            {
                ShipHealth shipHealth = other.GetComponent<ShipHealth>();
                if (shipHealth != null) shipHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
