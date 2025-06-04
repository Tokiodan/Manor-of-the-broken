using UnityEngine;

public class DeathUIManager : MonoBehaviour
{
    public GameObject deathUI;

    private CharacterController characterController;

    public void Initialize(GameObject player)
    {
        if (player != null)
            characterController = player.GetComponent<CharacterController>();

        if (deathUI != null)
            deathUI.SetActive(false);
    }

    public void PlayerDied()
    {
        if (characterController != null)
            characterController.enabled = false;

        if (deathUI != null)
            deathUI.SetActive(true);
    }

    public void PlayerRespawned()
    {
        if (characterController != null)
            characterController.enabled = true;

        if (deathUI != null)
            deathUI.SetActive(false);
    }
}
