using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public GameObject player;
    public Transform checkpoint;

    private CharacterController characterController;

    protected virtual void Start()
    {
        if (player != null)
        {
            characterController = player.GetComponent<CharacterController>();
        }
    }

    public virtual void Respawn()
    {
        if (player == null || checkpoint == null) return;

        // Disable character controller to avoid conflicts during position set
        if (characterController != null)
            characterController.enabled = false;

        player.transform.position = checkpoint.position;

        if (characterController != null)
            characterController.enabled = true;
    }
}
