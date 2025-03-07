using UnityEngine;

public class GuidedProjectile : MonoBehaviour
{
    private Transform target;
    public float speed = 20f;
    public float rotationSpeed = 5f;
    public float damage = 20f;
    public float lifetime = 10f;
    public GameObject explosionEffect;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
