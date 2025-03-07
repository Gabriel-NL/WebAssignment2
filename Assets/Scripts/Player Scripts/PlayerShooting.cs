using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign in Inspector
    public Transform firePoint; // Assign a fire point (empty GameObject) at the front of the ship
    public float projectileSpeed = 20f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Left mouse button
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }
}
