using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Image staminaBar;

    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public StaminaSystem staminaSystem;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Death")
        {
            Destroy(gameObject);
        }
        else if (scene.name == "Respawn")
        {
            if (GameManager.Instance != null && GameManager.Instance.respawnPoint != null)
            {
                transform.position = GameManager.Instance.respawnPoint.position;
            }
        }
    }
}
