using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float angularSpeed = 500f;

    [SerializeField] float throwRange = 15f;

    [SerializeField] GameObject aimCamera;
    [SerializeField] Transform aimTarget;

    [SerializeField] Pokeball pokeballPrefab;

    Quaternion targetRotation;

    bool isRunning;
    bool isAiming;
    bool inAction;

    float aimAngle = 0f;

    Transform mainCamera;
    new Camera camera;
    Animator animator;
    CharacterController characterController;

    private void Awake()
    {
        camera = Camera.main;  // Correct way to access Camera.main
        mainCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (inAction)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(DoAction("Dive"));
        }
        if (Input.GetButtonDown("Throw"))
        {
            Aim();
        }
        else if (Input.GetButtonUp("Throw") && isAiming)
        {
            isAiming = false;
            animator.SetBool("isAiming", isAiming);
            StartCoroutine(DoAction("Throw", () => aimCamera.SetActive(false)));
            ThrowPokeball(); // Ensure this is being called
        }


        // Movement logic
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        Vector3 moveInput = new Vector3(h, 0, v);
        float camYRotation = mainCamera.rotation.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;

        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        if (isAiming)
        {
            // Horizontal Aiming - Rotate the player
            float rotationY = transform.rotation.eulerAngles.y;
            rotationY += Input.GetAxis("Camera X") * angularSpeed * Time.deltaTime;
            targetRotation = Quaternion.Euler(0, rotationY, 0);

            // Vertical Aiming
            aimAngle += Input.GetAxis("Camera Y");
            aimAngle = Mathf.Clamp(aimAngle, -60f, 60f); // Limit vertical aiming
            aimTarget.localRotation = Quaternion.Lerp(aimTarget.localRotation, Quaternion.Euler(aimAngle, 0, 0), Time.deltaTime * angularSpeed);
        }
        else
        {
            if (moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDir);
            }
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
        animator.SetFloat("moveAmount", moveAmount * moveSpeed / runSpeed, 0.2f, Time.deltaTime);
    }

    private IEnumerator DoAction(string animName, Action onOver = null)
    {
        inAction = true;
        animator.CrossFade(animName, 0.2f);
        yield return null;

        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        float timer = 0f;

        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            if (animator.IsInTransition(0) && timer > 0.4f)
                break;

            yield return null;
        }

        inAction = false;
        onOver?.Invoke();
    }

    // Aim the Pokeball
    Pokeball pokeballObj;
    private void Aim()
    {
        isAiming = true;
        animator.SetBool("isAiming", isAiming);
        aimCamera.SetActive(true);

        // Ensure the pokeballPrefab is assigned
        if (pokeballPrefab == null)
        {
            Debug.LogError("Pokeball prefab is not assigned!");
            return;
        }

        // Instantiate the Pokeball and parent it to the right hand
        pokeballObj = Instantiate(pokeballPrefab, animator.GetBoneTransform(HumanBodyBones.RightHand));
    }

    Vector3 targetPos;

    // Throw the Pokeball
    void ThrowPokeball()
    {
        // Debugging raycast origin and direction
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));  // Center of the camera viewport
        Debug.Log($"Ray Origin: {rayOrigin}, Camera Direction: {camera.transform.forward}");

        // Cast the ray to detect the target position
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out RaycastHit hit, throwRange))
        {
            targetPos = hit.point;
            Debug.Log($"Hit target at {targetPos}");
        }
        else
        {
            targetPos = rayOrigin + camera.transform.forward * throwRange;
            Debug.Log($"No target hit. Defaulting to {targetPos}");
        }

        // Launch the Pokeball to the target position
        pokeballObj.LaunchToTarget(targetPos);
    }
}
