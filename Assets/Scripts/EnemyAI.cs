using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    [Header("Player Settings")]
    public Transform player;
    public float detectionRadius = 15f;
    public float fieldOfViewAngle = 120f;

    [Header("Attack Settings")]
    public float attackRange = 2f;         // Reduced for melee
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public float EnemyDamage = 10f;

    private enum AIState { Patrolling, Chasing, Attacking }
    private AIState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        currentState = AIState.Patrolling;

        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void Update()
    {
        LookForPlayer();

        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol();
                break;
            case AIState.Chasing:
                Chase();
                break;
            case AIState.Attacking:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void LookForPlayer()
    {
        if (player == null)
            return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < detectionRadius)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                if (currentState != AIState.Attacking)
                    currentState = AIState.Chasing;
            }
        }
        else
        {
            if (currentState != AIState.Patrolling)
            {
                currentState = AIState.Patrolling;
                if (patrolPoints.Length > 0)
                    agent.destination = patrolPoints[currentPatrolIndex].position;
            }
        }
    }

    void Chase()
    {
        if (player == null)
            return;

        agent.destination = player.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = AIState.Attacking;
        }
        else if (distanceToPlayer > detectionRadius)
        {
            currentState = AIState.Patrolling;
            if (patrolPoints.Length > 0)
                agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void Attack()
    {
        if (player == null)
            return;

        agent.destination = transform.position;

        Vector3 lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookDirection);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            currentState = AIState.Chasing;
            return;
        }

        if (Time.time >= nextFireTime)
        {
            PerformMeleeAttack();
            nextFireTime = Time.time + fireRate;
        }
    }

    // Melee attack instead of shooting
    void PerformMeleeAttack()
    {
        Debug.Log("Enemy performing melee attack!");

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerHP playerHP = player.GetComponent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(EnemyDamage);
                Debug.Log("Player hit by melee attack! Damage applied.");
            }

            Debug.Log("Player would be damaged here if PlayerHP was active.");
        }
    }
}
