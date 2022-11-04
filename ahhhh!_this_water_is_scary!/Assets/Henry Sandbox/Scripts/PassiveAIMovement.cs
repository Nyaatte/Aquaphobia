using UnityEngine;
using UnityEngine.AI;

public class PassiveAIMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsGround;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
  

    private void Awake()
    {
        // Find Player Object
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
    Patrolling();
    }

    private void Patrolling()
    {   // if no walkpoint, search walkpoint
        if (!walkPointSet) SearchWalkPoint();
        // if walkpoint set, go to walkpoint
        if (walkPointSet)
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        
    }
    private void SearchWalkPoint()
    {   // Calculate random spot in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check Point with Raycast
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }
}
