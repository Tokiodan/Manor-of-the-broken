using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float sprintStaminaDrain = 20f;
    public float vaultStaminaCost = 30f;
    public float staminaRegen = 10f;

    public float CurrentStamina { get; private set; }
    private float cooldownTimer;
    private Image staminaBar;

    void Start()
    {
        CurrentStamina = maxStamina;
        staminaBar = GetComponent<PlayerController>().staminaBar;
    }

    public void UseStamina(float amount)
    {
        CurrentStamina -= amount * Time.deltaTime;
        if (CurrentStamina <= 0f)
        {
            CurrentStamina = 0;
            cooldownTimer = 5f;
        }
        UpdateUI();
    }

    public void RecoverStamina(bool canRecover)
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
        else if (canRecover && CurrentStamina < maxStamina)
            CurrentStamina += staminaRegen * Time.deltaTime;

        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);
        UpdateUI();
    }

    public bool CanSprint() => cooldownTimer <= 0f && CurrentStamina > 0f;

    private void UpdateUI()
    {
        if (staminaBar != null)
            staminaBar.fillAmount = CurrentStamina / maxStamina;
    }
}