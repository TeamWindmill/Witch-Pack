using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class UnitMovement : MonoBehaviour
{
    private bool reachedDest;
    private Vector2 currentDest;
    public Action OnDestenationSet;
    public Action OnDestenationReached;
    private Coroutine activeMovementRoutine;
    private BaseUnit owner;
    [SerializeField] private NavMeshAgent agent;


    public bool IsMoving => agent.velocity.sqrMagnitude > 0; //need to replace

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
        agent.velocity = Vector3.zero;
        currentDest = transform.position;
        currentDest = worldPos;
        reachedDest = false;
        OnDestenationSet?.Invoke();
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
        yield return new WaitUntil(() => agent.velocity != Vector3.zero);
        yield return new WaitUntil(() =>  agent.remainingDistance <= agent.stoppingDistance);
        OnDestenationReached?.Invoke();
    }



   
}
