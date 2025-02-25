using System.Collections;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public float reverseSpeed = 2f;
    public float rotationSpeed = 100f;
    public GameObject captureOrbPrefab;
    public Transform firePoint;

    private float currentSpeed = 0f;
    private bool isReversing = false;
    private Vector3 movementDirection = Vector3.zero;

    void Update()
    {
        HandleMovement();
        HandleActions();
    }

    void HandleMovement()
    {
        // Accelerate with SHIFT to move along the X and Z-axis
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            movementDirection = transform.right; // Move along X-axis
        }
        // Decelerate with SPACE
        else if (Input.GetKey(KeyCode.Space))
        {
            if (currentSpeed > 0)
            {
                currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.deltaTime, 0);
                isReversing = false;
            }
            else
            {
                StartCoroutine(EnableReverseMode());
            }
        }
        // Move backward slowly if holding space after 0.5s
        else if (isReversing)
        {
            currentSpeed = -reverseSpeed;
        }

        // Tilt up and down using W and S
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

        // Rotate left and right using A and D
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

        // Apply movement along the X and Z-axis
        transform.position += movementDirection * currentSpeed * Time.deltaTime;
    }

    void HandleActions()
    {
        // Open inventory with E
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
        }

        // Shoot capture orb with C
        if (Input.GetKeyDown(KeyCode.C))
        {
            ShootCaptureOrb();
        }

        // Use special consumable with Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSpecialConsumable();
        }
    }

    IEnumerator EnableReverseMode()
    {
        yield return new WaitForSeconds(0.5f);
        isReversing = Input.GetKey(KeyCode.Space);
    }

    void OpenInventory()
    {
        Debug.Log("Inventory opened!");
        // Implement inventory logic here
    }

    void ShootCaptureOrb()
    {
        if (captureOrbPrefab && firePoint)
        {
            Instantiate(captureOrbPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Capture orb fired!");
        }
    }

    void UseSpecialConsumable()
    {
        Debug.Log("Special consumable used!");
        // Implement consumable logic here
    }
}
