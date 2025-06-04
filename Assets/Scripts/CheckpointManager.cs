using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public GameObject player;
    public Transform checkpoint;
    public GameObject deathUI;

    private CharacterController characterController;
    private PlayerController playerController;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (deathUI != null)
            deathUI.SetActive(false);

        if (player != null)
        {
            characterController = player.GetComponent<CharacterController>();
            playerController = player.GetComponent<PlayerController>();
        }
    }

    public void PlayerDied()
    {
        if (characterController != null)
            characterController.enabled = false;

        if (deathUI != null)
            deathUI.SetActive(true);
    }

    public void Respawn()
    {
        if (checkpoint == null || player == null) return;

        if (characterController != null)
            characterController.enabled = false;

        // Move player to checkpoint position
        player.transform.position = checkpoint.position;

        // Allow frame delay before re-enabling the controller
        StartCoroutine(EnableControllerNextFrame());

        if (deathUI != null)
            deathUI.SetActive(false);
    }

    private IEnumerator EnableControllerNextFrame()
    {
        yield return null; // wait 1 frame
        if (characterController != null)
            characterController.enabled = true;
    }

}
