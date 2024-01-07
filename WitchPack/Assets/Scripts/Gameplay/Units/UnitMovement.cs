using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class UnitMovement : MonoBehaviour
{
    private bool reachedDest;
    private Vector2 currentDest;
    public Action<Vector3> OnDestenationSet;
    public Action<Vector3> OnDestenationReached;
    private Coroutine activeMovementRoutine;
    private BaseUnit owner;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetUp(BaseUnit givenOwner)
    {
        owner = givenOwner;

    }

    public void SetSpeed(float value)
    {
        agent.speed = value;
    }

    public void AddSpeed(StatType stat, float value)
    {
        if (stat == StatType.MovementSpeed)
        {
            agent.speed += value;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            SetDest(newDest);
        }
    }

    public void SetDest(Vector3 worldPos)
    {
        currentDest = worldPos;
        reachedDest = false;
        OnDestenationSet?.Invoke(worldPos);
        agent.destination = (Vector2)worldPos;
        if (!ReferenceEquals(activeMovementRoutine, null))
        {
            StopCoroutine(activeMovementRoutine);
        }
        activeMovementRoutine = StartCoroutine(WaitTilReached());
    }

    private IEnumerator WaitTilReached()
    {
        yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);
        OnDestenationReached?.Invoke(transform.position);
    }



   
}
