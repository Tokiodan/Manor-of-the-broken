using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float maxHP = 100f;
    private float currentHP;

    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime = -999f;

    [Header("Damage Settings")]
    public float attackRange = 2f;  // Optional: fallback damage range check
    private Transform player;

    void Start()
    {
        currentHP = maxHP;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        // Optional distance check damage if no trigger used or trigger missed
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                TryDealDamage(player.gameObject);
            }
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryDealDamage(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryDealDamage(other.gameObject);
        }
    }

    void TryDealDamage(GameObject playerObj)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHP player = playerObj.GetComponent<PlayerHP>();
            if (player != null && !player.isDead)
            {
                player.TakeDamage(damage);
                lastAttackTime = Time.time;
                Debug.Log("Enemy dealt damage to player!");
            }
        }
    }
}
