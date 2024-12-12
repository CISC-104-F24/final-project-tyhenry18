using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [SerializeField] private GameObject pokemonPrefab; // Assign this in the Inspector
    [SerializeField] private LayerMask terrainLayer;   // Assign this in the Inspector

    Transform cam;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    public void LaunchToTarget(Vector3 targetPos)
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        Vector3 launchVelocity = CalculateLaunchVelocity(targetPos);
        GetComponent<Rigidbody>().velocity = launchVelocity;
        Debug.Log("Launching Pokeball with velocity: " + launchVelocity);
    }


    private Vector3 CalculateLaunchVelocity(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;

        float displacementY = targetPos.y - startPos.y;
        Vector3 displacementXZ = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);

        float h = Mathf.Abs(displacementY) + 1;
        float g = Physics.gravity.y;

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (displacementY - h) / g));

        return velocityY + velocityXZ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        Vector3 rayOrigin = transform.position + Vector3.up;
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, terrainLayer))
        {
            Instantiate(pokemonPrefab, hit.point, Quaternion.identity);

            var dirToCam = (cam.position - hit.point).normalized;
            dirToCam.y = 0;
            pokemonPrefab.transform.forward = dirToCam;
        }

        Destroy(gameObject);
    }
}
