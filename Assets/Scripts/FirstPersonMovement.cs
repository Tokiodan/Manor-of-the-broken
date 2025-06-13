using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;// Crouch speed in deze script = Hoe snel je loopt terwijl je croucht
    public float gravity = 20f;

    private PlayerController player;
    public Vector3 moveDirection;

    void Start() { player = GetComponent<PlayerController>(); }

    void Update()
    {
        if (GetComponent<VaultController>().IsVaulting) return;
        float speed = walkSpeed;
        bool canSprint = player.staminaSystem.CanSprint();
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint && !GetComponent<CrouchController>().IsCrouching && player.characterController.isGrounded;

        if (isSprinting)
        {
            speed = sprintSpeed;
            player.staminaSystem.UseStamina(player.staminaSystem.sprintStaminaDrain);
        }
        else if (GetComponent<CrouchController>().IsCrouching)
        {
            speed = crouchSpeed;
        }

        player.staminaSystem.RecoverStamina(!isSprinting);

        float moveY = moveDirection.y;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float moveX = speed * Input.GetAxis("Vertical");
        float moveZ = speed * Input.GetAxis("Horizontal");
        moveDirection = forward * moveX + right * moveZ;

        moveDirection.y = player.characterController.isGrounded ? -1f : moveY - gravity * Time.deltaTime;
        player.characterController.Move(moveDirection * Time.deltaTime);
    }

    public void ResetMovement()
    {
        moveDirection = Vector3.zero;
    }

}
