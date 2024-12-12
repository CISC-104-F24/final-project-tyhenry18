using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    // SerializeField allows you to assign these fields in the Unity Inspector
    [SerializeField] private GameObject pokemonPrefab; // Reference to the Pok�mon prefab to spawn when the Pok�ball lands
    [SerializeField] private LayerMask terrainLayer;   // Layer to check for terrain collision when the Pok�ball lands

    Transform cam;  // Reference to the camera's transform (used for orientation after landing)
    private Rigidbody rb;  // Reference to the Rigidbody component for physics-based movement

    private void Awake()
    {
        // Initialize the Rigidbody and Camera references
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;  // Set the camera to the main camera in the scene
    }

    // Method to launch the Pok�ball towards a target position
    public void LaunchToTarget(Vector3 targetPos)
    {
        // Detach the Pok�ball from its parent and enable physics
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;  // Enable physics simulation for movement

        // Calculate the velocity needed to launch the Pok�ball to the target position
        Vector3 launchVelocity = CalculateLaunchVelocity(targetPos);

        // Apply the calculated velocity to the Rigidbody
        GetComponent<Rigidbody>().velocity = launchVelocity;

        // Log the launch velocity for debugging purposes
        Debug.Log("Launching Pokeball with velocity: " + launchVelocity);
    }

    // Method to calculate the velocity required to launch the Pok�ball to the target
    private Vector3 CalculateLaunchVelocity(Vector3 targetPos)
    {
        // Get the starting position of the Pok�ball
        Vector3 startPos = transform.position;

        // Calculate the vertical displacement and the horizontal displacement (XZ plane)
        float displacementY = targetPos.y - startPos.y;
        Vector3 displacementXZ = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);

        // Calculate the height needed to launch the Pok�ball
        float h = Mathf.Abs(displacementY) + 1;  // Add a small buffer to height for more realistic physics

        // Get the gravity value to use in the calculations
        float g = Physics.gravity.y;

        // Calculate the vertical (Y-axis) velocity required
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);

        // Calculate the horizontal (XZ-plane) velocity required
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (displacementY - h) / g));

        // Return the combined launch velocity (Y + XZ components)
        return velocityY + velocityXZ;
    }

    // Method to handle the collision when the Pok�ball hits something
    private void OnCollisionEnter(Collision collision)
    {
        // If the Pok�ball collides with the Player, don't do anything
        if (collision.gameObject.CompareTag("Player")) return;

        // Create a ray starting slightly above the Pok�ball to check the terrain underneath
        Vector3 rayOrigin = transform.position + Vector3.up;

        // Perform a raycast downward to check for the terrain layer
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, terrainLayer))
        {
            // Instantiate the Pok�mon prefab at the point where the raycast hit the ground
            Instantiate(pokemonPrefab, hit.point, Quaternion.identity);

            // Calculate the direction to the camera and set the Pok�mon's orientation
            var dirToCam = (cam.position - hit.point).normalized;
            dirToCam.y = 0;  // Ignore vertical component to keep the Pok�mon oriented horizontally
            pokemonPrefab.transform.forward = dirToCam;  // Set the Pok�mon to face the camera
        }

        // Destroy the Pok�ball object once it has landed and spawned the Pok�mon
        Destroy(gameObject);
    }
}
