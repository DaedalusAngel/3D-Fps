using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;

    // How close the player can get before the enemy takes notice
    [SerializeField] float chaseRange = 5.0f;

    NavMeshAgent nma;

    // Start by setting a value equal to the entire Scene so that
    // it doesn't matter where an enemy is placed. This can also
    // be used as a comparison with the chaseRange variable
    float distanceToTarget = Mathf.Infinity;

    private bool isProvoked = false;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Measures the distance between the enemy and the player
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= nma.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= nma.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        nma.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        // Temp for now, to show that the enemy is attacking
        print(name + " is attacking " + target.name);
    }

    void OnDrawGizmosSelected()
    {
        // Display the chase radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}