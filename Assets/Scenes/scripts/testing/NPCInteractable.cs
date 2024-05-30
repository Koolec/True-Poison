using UnityEngine;
using UnityEngine.AI;

public class NPCInteractable : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isMoving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 1.5f;
    }

    private void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0)
        {
            StartWalking();
        }
        else
        {
            StopWalking();
        }
    }

    private void StartWalking()
    {
        if (!isMoving)
        {
            isMoving = true;
            animator.SetTrigger("Walk");
        }
    }

    private void StopWalking()
    {
        if (isMoving)
        {
            isMoving = false;
            animator.SetTrigger("Idle");
        }
    }

    public void Interact()
    {
        animator.SetTrigger("Talk");
    }
}