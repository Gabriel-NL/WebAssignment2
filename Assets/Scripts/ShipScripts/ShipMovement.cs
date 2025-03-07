using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class ShipMovement : MonoBehaviour
{
    private Rigidbody player_ship_rigidbody;
    public PlayerInput playerInput;

    public float maxSpeed = 200f;
    public float accelerationRate = 10f;  // The rate at which speed increases
    public float decelerationRate = 20f;  // The rate at which speed decreases when brakes are applied
    public float currentSpeed = 0f;  // Current speed of the ship

    public float turnSpeed = 100f;
    private int[] rotation_directions = new int[4];
    private Vector3 rotation = new Vector3(0, 0, 0);
    private bool accelerate = false, brakes_activated = false;

    private bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    private bool invOpen = false;
    private Dismantle dismantle_script;
    [SerializeField] GameObject inventoryUI;

    public bool isCoroutineRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dismantle_script=FindFirstObjectByType<Dismantle>();
        player_ship_rigidbody = GetComponent<Rigidbody>();
        InitializeRotationHandlers();
    }

    void FixedUpdate()
    {
        ChangeRotation(rotation_directions);
        Accelerate();
        if (Input.GetMouseButton(2) && isCoroutineRunning == false) // MouseButton 2 is the middle click (center mouse button)
        {
            Debug.Log("Activating");
            StartCoroutine(Stabilizer());
        }
    }

    private void InitializeRotationHandlers()
    {
        playerInput.actions["turn_ship_up"].started += _ => rotation_directions[0] = -1;
        playerInput.actions["turn_ship_up"].canceled += _ => rotation_directions[0] = 0;

        playerInput.actions["turn_ship_down"].started += _ => rotation_directions[1] = 1;
        playerInput.actions["turn_ship_down"].canceled += _ => rotation_directions[1] = 0;

        playerInput.actions["turn_ship_left"].started += _ => rotation_directions[2] = -1;
        playerInput.actions["turn_ship_left"].canceled += _ => rotation_directions[2] = 0;

        playerInput.actions["turn_ship_right"].started += _ => rotation_directions[3] = 1;
        playerInput.actions["turn_ship_right"].canceled += _ => rotation_directions[3] = 0;

        playerInput.actions["accelerate"].started += _ => accelerate = true;
        playerInput.actions["accelerate"].canceled += _ => accelerate = false;

        playerInput.actions["deccelerate"].started += _ => brakes_activated = true;
        playerInput.actions["deccelerate"].canceled += _ => brakes_activated = false;

        playerInput.actions["pause_unpause"].performed += PauseUnpause;
        playerInput.actions["inventory"].performed += inventoryOpenClose;
    }

    private void ChangeRotation(int[] directions)
    {
        rotation.x = directions[0] + directions[1];
        rotation.y = directions[2] + directions[3];
        // Apply rotation to the Rigidbody (use MoveRotation for Rigidbody)
        Quaternion targetRotation = Quaternion.Euler(rotation * turnSpeed * Time.deltaTime);
        player_ship_rigidbody.MoveRotation(player_ship_rigidbody.rotation * targetRotation);
    }

    private void Accelerate()
    {
        // Acceleration logic
        if (accelerate && currentSpeed < maxSpeed)
        {
            // Increase speed slowly
            currentSpeed += accelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);  // Clamp the speed to the maximum
        }

        // Braking logic (deceleration)
        if (brakes_activated && currentSpeed > 0)
        {
            // Decrease speed (twice as fast as acceleration)
            currentSpeed -= decelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  // Ensure speed doesn't go negative
        }

        // Apply movement using the Rigidbody (move forward based on the current speed)
        player_ship_rigidbody.linearVelocity = transform.forward * currentSpeed;
    }

    private void PauseUnpause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            if (invOpen)
            {
                inventoryUI.SetActive(false);
                invOpen = false;
            }
        }
        isPaused = !isPaused;
    }

    private void inventoryOpenClose(InputAction.CallbackContext context)
    {
        if (!invOpen && !isPaused)
        {
            inventoryUI.SetActive(true);
            invOpen = true;
        }
        else
        {
            inventoryUI.SetActive(false);
            invOpen = false;
        }
    }

    private IEnumerator Stabilizer()
    {
        isCoroutineRunning = true;

        // Get the starting Z rotation
        float startZ = transform.rotation.eulerAngles.z;
        float targetZ = 0f;  // The target Z rotation (reset to 0)
        float speed = 100f;    // Adjust speed for how fast you want the rotation

        // Gradually rotate the Z value towards 0
        while (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, targetZ)) > 0.01f)
        {
            // Move towards the target Z rotation
            float newZ = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetZ, speed * Time.deltaTime);

            // Apply the new rotation to the object (keep other axis rotations unchanged)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newZ);

            yield return null;  // Wait for the next frame
        }

        // Ensure the final z rotation is exactly 0
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetZ);
        isCoroutineRunning = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MotherShip"))
        {
            currentSpeed = 0;
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider.CompareTag("ShipNose"))
            {
                //Debug.Log("Nose took damage!");
                dismantle_script.ExplodeAndDismantle();
            }
        }
    }
}
