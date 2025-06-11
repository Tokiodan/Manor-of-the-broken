using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public bool isDead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        // Add death logic (animation, respawn, etc.)
    }

    public void Regenerate(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log($"Player healed for {amount}. Current health: {currentHealth}");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Medkit medkit = hit.gameObject.GetComponent<Medkit>();
        if (medkit != null)
        {
            if(currentHealth < maxHealth)
            {
                Regenerate(medkit.GetRecoverAmount());
                Destroy(hit.gameObject);
            }
        }
    }


}