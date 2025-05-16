using UnityEngine;
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
}