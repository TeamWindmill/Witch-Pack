using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class UnitMovement : MonoBehaviour
{
    public event Action OnDestinationSet;
    public event Action OnDestinationReached;
    
    public float DefaultStoppingDistance { get; private set; }
    
    private Vector3 currentDest;
    private Coroutine activeMovementRoutine;
    private BaseUnit owner;
    [SerializeField] private NavMeshAgent agent;

    public NavMeshAgent Agent => agent;
    public bool IsMoving => agent.velocity.sqrMagnitude > 0; 
    public bool IsActive => agent.enabled; 

    public Vector3 CurrentDestination => currentDest;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        DefaultStoppingDistance = agent.stoppingDistance;
    }

    public void SetUp(BaseUnit givenOwner)
    {
        owner = givenOwner;
        ToggleMovement(true);
    }

    private void Update()
    {
        agent.speed = owner.Stats.MovementSpeed * GAME_TIME.TimeRate;
    }

    public void SetDestination(Vector3 worldPos)
    {
        if(!agent.isOnNavMesh || !agent.enabled) return;
        currentDest = worldPos;
        OnDestinationSet?.Invoke();
        agent.destination = (Vector2)worldPos;
        if (!ReferenceEquals(activeMovementRoutine, null))
        {
            StopCoroutine(activeMovementRoutine);
        }
        activeMovementRoutine = StartCoroutine(WaitTilReached());
        
    }


    public void ToggleMovement(bool state)
    {
        agent.enabled = state;
    }

    private IEnumerator WaitTilReached()
    {
        yield return new WaitUntil(CheckVelocity);
        yield return new WaitUntil(CheckRemainingDistance);
        OnDestinationReached?.Invoke();
    }

    private bool CheckRemainingDistance()
    {
        if (!agent.enabled) return true;
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    private bool CheckVelocity()
    {
        if (!agent.enabled) return true;
        return agent.velocity != Vector3.zero;
    }


   
}
