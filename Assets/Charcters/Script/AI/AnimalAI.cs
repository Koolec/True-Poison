using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent _navMashAgent;
    private Animator _animator;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float changePositionTime =7f;
    [SerializeField] private float moveDistance = 10f;

    private void Start()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();
        _navMashAgent.speed = movementSpeed;
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(MoveAnimal), changePositionTime, changePositionTime);
    }

    public void Update() {
         _animator.SetFloat("Speed", _navMashAgent.velocity.magnitude/movementSpeed);
    }

        Vector3 RandomNavSphere ( float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition ( randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void MoveAnimal()
    {
        _navMashAgent.SetDestination(RandomNavSphere(moveDistance));
       
    }
}
