using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float gravity = 20f;

    [Header("Mouse Look Settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;

    [Header("Crouch Settings")]
    public float crouchScale = 0.5f; // The scale factor for crouching
    public float standingScale = 1f; // Normal scale for standing (no crouch)
    public float crouchTransitionSpeed = 10f; // Speed of crouch transition
    public float crouchCameraOffset = -0.5f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float sprintStaminaDrain = 20f;
    public float vaultStaminaCost = 30f;
    public float staminaRegen = 10f;
    public Image staminaBar;

    [Header("Vault Settings")]
    public float vaultDistance = 1.5f;
    public float vaultHeight = 1f;
    public float vaultDuration = 0.5f;

    private float currentStamina;
    private float rotationX = 0;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Camera playerCamera;
    private bool isCrouching = false;
    private bool isVaulting = false;
    private Vector3 cameraStandPosition;
    private Vector3 cameraCrouchPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = maxStamina;

        cameraStandPosition = playerCamera.transform.localPosition;
        cameraCrouchPosition = cameraStandPosition + new Vector3(0, crouchCameraOffset, 0);
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCrouch();
        HandleVault();
        UpdateStaminaUI();
    }

    void HandleMouseLook()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleMovement()
    {
        if (isVaulting) return;

        float speed = walkSpeed;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isCrouching && characterController.isGrounded;

        if (isSprinting)
        {
            speed = sprintSpeed;
            currentStamina -= sprintStaminaDrain * Time.deltaTime;
        }
        else if (isCrouching)
        {
            speed = crouchSpeed;
        }

        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        float moveDirectionY = moveDirection.y;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = speed * Input.GetAxis("Vertical");
        float curSpeedY = speed * Input.GetAxis("Horizontal");
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!characterController.isGrounded)
        {
            moveDirection.y = moveDirectionY - gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = -1f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;

            // Scale the object down when crouching, and back up when standing.
            Vector3 targetScale = isCrouching ? new Vector3(1, crouchScale, 1) : new Vector3(1, standingScale, 1);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, crouchTransitionSpeed * Time.deltaTime);

            // Adjust the camera position to match the crouch
            Vector3 targetCameraPosition = isCrouching ? cameraCrouchPosition : cameraStandPosition;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, targetCameraPosition, crouchTransitionSpeed * Time.deltaTime);
        }
    }

    void HandleVault()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isVaulting && currentStamina >= vaultStaminaCost)
        {
            RaycastHit hit;
            Vector3 origin = transform.position + Vector3.up * 0.5f;
            if (Physics.Raycast(origin, transform.forward, out hit, vaultDistance))
            {
                if (hit.transform.CompareTag("Vaultable"))
                {
                    StartCoroutine(VaultOverObstacle(hit));
                }
            }
        }
    }

    IEnumerator VaultOverObstacle(RaycastHit hit)
    {
        isVaulting = true;
        currentStamina -= vaultStaminaCost;
        Vector3 startPos = transform.position;
        Vector3 endPos = hit.point + Vector3.up * vaultHeight + transform.forward * 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < vaultDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / vaultDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isVaulting = false;
    }

    void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / maxStamina;
        }
    }
}
