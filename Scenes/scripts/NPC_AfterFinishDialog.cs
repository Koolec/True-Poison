using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPC_AfterFinishDialog : MonoBehaviour
{
    public GameObject emptyDestination;

    private bool isMoving = false;

    void Update()
    {
        if (GameObject.Find("FinishDialog_1") && !isMoving)
        {
            MoveToEmptyDestination();
            isMoving = true;
        }

        if (isMoving && Vector3.Distance(transform.position, emptyDestination.transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    public void MoveToEmptyDestination()
    {
        StartCoroutine(MoveToEmptyDestinationWithDelay(3f));
    }

    private IEnumerator MoveToEmptyDestinationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(emptyDestination.transform.position);
            navMeshAgent.isStopped = false;
        }
        else
        {
            Debug.LogError("NavMeshAgent component is not attached to this GameObject! Please attach the NavMeshAgent component.");
        }
    }
}