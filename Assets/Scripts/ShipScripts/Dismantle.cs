using UnityEngine;

public class Dismantle : MonoBehaviour
{

    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float torqueAmount = 10f;

    public void ExplodeAndDismantle()
    {
        // Get all child parts
        foreach (Transform part in transform)
        {
            if (part.GetComponent<Camera>())
            {
                continue;
            }
            
            if (part.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
            {
                rb = part.gameObject.AddComponent<Rigidbody>(); // Add Rigidbody if not present
            }

            part.SetParent(null); // Detach from parent
            rb.isKinematic = false; // Enable physics
            rb.useGravity = false; // Disable gravity for space effect

            // Apply explosion force
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            // Apply random rotation
            Vector3 randomTorque = new Vector3(
                Random.Range(-torqueAmount, torqueAmount),
                Random.Range(-torqueAmount, torqueAmount),
                Random.Range(-torqueAmount, torqueAmount)
            );
            rb.AddTorque(randomTorque, ForceMode.Impulse);
        }
    }
}
