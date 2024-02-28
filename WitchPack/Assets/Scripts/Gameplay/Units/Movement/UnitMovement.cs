using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class UnitMovement : MonoBehaviour
{
    public event Action OnDestinationSet;
    public event Action OnDestinationReached;
    
    private Vector3 currentDest;
    private Coroutine activeMovementRoutine;
    private BaseUnit owner;
    [SerializeField] private NavMeshAgent agent;

    public NavMeshAgent Agent => agent;
    public bool IsMoving => agent.velocity.sqrMagnitude > 0; //need to replace
    public float StoppingDistance => agent.stoppingDistance; //need to replace

    public Vector3 CurrentDestination => currentDest;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetUp(BaseUnit givenOwner)
    {
        owner = givenOwner;
        ToggleMovement(true);
    }

   /* public void SetSpeed(float value)
    {
        agent.speed = value;
        //agent.acceleration = agent.speed;
    }


    public void AddSpeed(StatType stat, float value)
    {
        if (stat == StatType.MovementSpeed)
        {
            agent.speed += value;
            //agent.acceleration = agent.speed;
        }
    }*/

    private void Update()
    {
        agent.speed = owner.Stats.MovementSpeed * GAME_TIME.GetCurrentTimeRate;
    }

    public void SetDest(Vector3 worldPos)
    {
        if(!agent.enabled || !agent.isOnNavMesh) return;
        agent.velocity = Vector3.zero;
        currentDest = worldPos;
        OnDestinationSet?.Invoke();
        if(owner is Shaman) Debug.Log("DestinationSet");
        agent.SetDestination(worldPos);
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
        if(owner is Shaman) Debug.Log("DestinationReached");
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
