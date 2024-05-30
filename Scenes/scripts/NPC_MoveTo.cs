using UnityEngine;
using UnityEngine.AI;

public class NPC_MoveTo : MonoBehaviour
{
    public Transform targetPointB;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        MoveTo(targetPointB.position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {

            navMeshAgent.isStopped = true;
        }
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;
    }
}