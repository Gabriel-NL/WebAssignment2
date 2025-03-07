using UnityEngine;

public class AlienShip : MonoBehaviour
{
    public Transform player;
    public float speed = 10f;
    public float detectionRange = 50f;
    public float shootingRange = 30f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;

    private float fireCooldown;

    void Update()
    {
        if (player == null) return;

        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(player);

        // Shooting Logic
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= shootingRange && distance >= shootingRange / 2)
        {
            if (fireCooldown <= 0)
            {
                Shoot();
                fireCooldown = fireRate;
            }
        }

        fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        GuidedProjectile guidedProjectile = projectile.GetComponent<GuidedProjectile>();
        if (guidedProjectile)
        {
            guidedProjectile.SetTarget(player);
        }
    }
}
