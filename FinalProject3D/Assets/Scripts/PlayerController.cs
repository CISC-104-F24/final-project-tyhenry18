using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // SerializeField allows you to set values in the Unity Inspector
    [SerializeField] float walkSpeed = 3f;  // Walking speed of the player
    [SerializeField] float runSpeed = 6f;   // Running speed of the player
    [SerializeField] float angularSpeed = 500f;  // Rotation speed when aiming

    [SerializeField] float throwRange = 15f;  // The maximum distance the Pokéball can be thrown

    [SerializeField] GameObject aimCamera;  // Camera used for aiming
    [SerializeField] Transform aimTarget;  // Target where the player aims the Pokéball

    [SerializeField] Pokeball pokeballPrefab;  // Reference to the Pokéball prefab to spawn when aiming

    Quaternion targetRotation;  // To store the target rotation for the player

    bool isRunning;  // Whether the player is running or not
    bool isAiming;  // Whether the player is aiming
    bool inAction;  // Whether the player is performing an action (e.g., jumping or throwing)

    float aimAngle = 0f;  // Vertical aiming angle for the camera

    Transform mainCamera;  // Reference to the main camera's transform
    new Camera camera;  // Reference to the main camera
    Animator animator;  // Animator for player animations
    CharacterController characterController;  // Character controller for movement

    private void Awake()
    {
        camera = Camera.main;  // Access the main camera
        mainCamera = Camera.main.transform;  // Access the camera's transform
        animator = GetComponent<Animator>();  // Access the Animator component
        characterController = GetComponent<CharacterController>();  // Access the CharacterController
    }

    private void Update()
    {
        if (inAction)  // If the player is in action (e.g., throwing or jumping), skip this update
            return;

        // Get player movement input (horizontal and vertical axes)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Toggle running on/off when the "Run" button is pressed
        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }

        // Start the "Dive" action if the "Jump" button is pressed
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(DoAction("Dive"));
        }

        // Start aiming if the "Throw" button is pressed
        if (Input.GetButtonDown("Throw"))
        {
            Aim();
        }
        // Stop aiming and throw the Pokéball when the "Throw" button is released
        else if (Input.GetButtonUp("Throw") && isAiming)
        {
            isAiming = false;
            animator.SetBool("isAiming", isAiming);  // Update the aiming animation state
            StartCoroutine(DoAction("Throw", () => aimCamera.SetActive(false)));  // Trigger throw animation and disable aiming camera
            ThrowPokeball();  // Ensure this is being called to throw the Pokéball
        }

        // Movement logic
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));  // Calculate movement intensity based on input
        Vector3 moveInput = new Vector3(h, 0, v);  // Movement vector (horizontal and vertical)
        float camYRotation = mainCamera.rotation.eulerAngles.y;  // Get the camera's rotation on the Y axis (to align player movement)
        Vector3 moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;  // Rotate movement input based on camera's Y rotation

        // Choose movement speed based on whether the player is running or walking
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);  // Move the player with the CharacterController

        // Aiming logic (if the player is aiming, rotate and adjust the camera)
        if (isAiming)
        {
            // Horizontal Aiming - Rotate the player left or right
            float rotationY = transform.rotation.eulerAngles.y;
            rotationY += Input.GetAxis("Camera X") * angularSpeed * Time.deltaTime;  // Rotate based on camera input
            targetRotation = Quaternion.Euler(0, rotationY, 0);  // Update the target rotation

            // Vertical Aiming - Adjust vertical angle of the camera
            aimAngle += Input.GetAxis("Camera Y");  // Update the vertical aiming angle
            aimAngle = Mathf.Clamp(aimAngle, -60f, 60f);  // Limit vertical aiming to prevent excessive tilting
            aimTarget.localRotation = Quaternion.Lerp(aimTarget.localRotation, Quaternion.Euler(aimAngle, 0, 0), Time.deltaTime * angularSpeed);  // Smooth the vertical aiming transition
        }
        else
        {
            // If not aiming, adjust rotation based on movement direction
            if (moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDir);  // Rotate to face the movement direction
            }
        }

        // Apply the rotation to the player
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

        // Set movement animation parameter based on speed
        animator.SetFloat("moveAmount", moveAmount * moveSpeed / runSpeed, 0.2f, Time.deltaTime);
    }

    // Coroutine to handle player actions like diving or throwing
    private IEnumerator DoAction(string animName, Action onOver = null)
    {
        inAction = true;  // Set the player as being in action
        animator.CrossFade(animName, 0.2f);  // Play the animation with a fade
        yield return null;

        // Wait for the animation to complete
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        float timer = 0f;

        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            // Break the loop if the animation is in transition
            if (animator.IsInTransition(0) && timer > 0.4f)
                break;

            yield return null;
        }

        inAction = false;  // Mark the action as complete
        onOver?.Invoke();  // Execute the callback (if any)
    }

    // Aim the Pokéball by enabling the aiming camera
    Pokeball pokeballObj;
    private void Aim()
    {
        isAiming = true;  // Set aiming state to true
        animator.SetBool("isAiming", isAiming);  // Update aiming animation state
        aimCamera.SetActive(true);  // Activate the aiming camera

        // Check if the Pokéball prefab is assigned
        if (pokeballPrefab == null)
        {
            Debug.LogError("Pokeball prefab is not assigned!");  // Log an error if not assigned
            return;
        }

        // Instantiate the Pokéball at the player's right hand
        pokeballObj = Instantiate(pokeballPrefab, animator.GetBoneTransform(HumanBodyBones.RightHand));
    }

    // Target position for throwing the Pokéball
    Vector3 targetPos;

    // Throw the Pokéball towards the target position
    void ThrowPokeball()
    {
        // Debugging raycast origin and direction
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));  // Center of the camera viewport
        Debug.Log($"Ray Origin: {rayOrigin}, Camera Direction: {camera.transform.forward}");

        // Cast the ray to detect the target position
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out RaycastHit hit, throwRange))
        {
            targetPos = hit.point;  // Set target position to the point where the ray hits
            Debug.Log($"Hit target at {targetPos}");
        }
        else
        {
            targetPos = rayOrigin + camera.transform.forward * throwRange;  // Set default target position if no hit
            Debug.Log($"No target hit. Defaulting to {targetPos}");
        }

        // Launch the Pokéball to the target position
        pokeballObj.LaunchToTarget(targetPos);
    }
}
