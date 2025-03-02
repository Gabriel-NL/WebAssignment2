using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public Transform target;  // Target (Player or Mothership)
    public float speed = 10f;  // Movement speed
    public float stoppingDistance = 5f;  // Distance before stopping
    public float rotationSpeed = 2f;  // Speed of rotation toward the target

    public GameObject projectilePrefab;  // Projectile to shoot
    public Transform firePoint;  // Where the projectile spawns
    public float shootRange = 5f;  // Shooting distance
    public float shootCooldown = 2f;  // Time between shots
    private float lastShotTime = 0f;

    void Start()
    {
        InvokeRepeating(nameof(FindClosestTarget), 0f, 1f); // Check for closest target every second
    }

    void Update()
    {
        if (target == null) return;

        // Rotate toward the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move toward the target if not close enough
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > stoppingDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

            if (target == null) return;

            if (distance <= shootRange && Time.time > lastShotTime + shootCooldown)
            {
                Debug.Log("Shooting..."); // Debugging line
                Shoot();
                lastShotTime = Time.time;
            }


    }

    void FindClosestTarget()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject Mothership = GameObject.FindGameObjectWithTag("MotherShip");

        if (Player != null && Mothership != null)
        {
            Debug.Log("Both Player and Mothership found.");
            target = (Vector3.Distance(transform.position, Player.transform.position) <
                      Vector3.Distance(transform.position, Mothership.transform.position))
                      ? Player.transform
                      : Mothership.transform;
        }
        else if (Player != null)
        {
            Debug.Log("Player found.");
            target = Player.transform;
        }
        else if (Mothership != null)
        {
            Debug.Log("Mothership found.");
            target = Mothership.transform;
        }
        else
        {
            Debug.Log("No target found.");
        }
    }


    void Shoot()
    {
        Debug.Log("Shoot() function called!"); // Debugging line

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectileClone = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Projectile Clone Created!"); // Debugging line

            Rigidbody rb = projectileClone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * 20f; // Set speed
            }
            else
            {
                Debug.LogError("Projectile clone is missing a Rigidbody!");
            }

            Physics.IgnoreCollision(projectileClone.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            Debug.LogError("Projectile Prefab or FirePoint is missing!");
        }
    }



}
