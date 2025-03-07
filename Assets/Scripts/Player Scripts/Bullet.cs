using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy projectile after a set time to avoid memory leaks
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
