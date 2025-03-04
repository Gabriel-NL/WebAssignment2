using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Assign your player in the Inspector
    public float distance = 5f; // Distance behind player
    public float height = 2f; // Height above player
    public float smoothSpeed = 100f; // Smooth follow speed
    public float rotationSpeed = 10f; // Speed of camera rotation

    private float yaw = 0f; // Camera rotation angle

    void Update()
    {
        // Get mouse input for free rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate target position behind the player
        Vector3 targetPosition = player.position - Quaternion.Euler(0, yaw, 0) * Vector3.forward * distance + Vector3.up * height;

        // Smoothly move towards target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Rotate to always look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
