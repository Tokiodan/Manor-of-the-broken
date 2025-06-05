using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP left: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
}
