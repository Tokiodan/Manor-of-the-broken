using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public bool isDead { get; private set; } = false;

    [Header("UI Feedback")]
    public Image damagePanel;
    public Image healPanel;
    public float fadeDuration = 1f;

    [Header("Persistent Damage Overlay")]
    public Image damagedLookPanel;

    void Start()
    {
        currentHealth = maxHealth;

        if (damagePanel != null)
        {
            damagePanel.gameObject.SetActive(false);
            SetImageAlpha(damagePanel, 1f);
        }

        if (healPanel != null)
        {
            healPanel.gameObject.SetActive(false);
            SetImageAlpha(healPanel, 1f);
        }

        if (damagedLookPanel != null)
        {
            SetImageAlpha(damagedLookPanel, 0f);
        }
    }

    void Update()
    {
        UpdateDamagedLook();
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");

        if (damagePanel != null)
            StartCoroutine(FadePanel(damagePanel));

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
        if (isDead)
            return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log($"Player healed for {amount}. Current health: {currentHealth}");

        if (healPanel != null)
            StartCoroutine(FadePanel(healPanel));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Medkit medkit = hit.gameObject.GetComponent<Medkit>();
        if (medkit != null)
        {
            if (currentHealth < maxHealth)
            {
                Regenerate(medkit.GetRecoverAmount());
                Destroy(hit.gameObject);
            }
        }
    }

    IEnumerator FadePanel(Image panel)
    {
        panel.gameObject.SetActive(true);
        SetImageAlpha(panel, 1f);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetImageAlpha(panel, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SetImageAlpha(panel, 0f);
        panel.gameObject.SetActive(false);
    }

    void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    void UpdateDamagedLook()
    {
        if (damagedLookPanel != null && !isDead)
        {
            float healthPercent = currentHealth / maxHealth;
            float damageAlpha = 1f - healthPercent; // Lower health = higher alpha
            SetImageAlpha(damagedLookPanel, damageAlpha);
        }
    }
}
