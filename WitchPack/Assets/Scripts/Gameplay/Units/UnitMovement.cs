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
        /* if (!ReferenceEquals(activeMovementRoutine, null))
         {
             StopCoroutine(activeMovementRoutine);
         }
         activeMovementRoutine = StartCoroutine(LerpToPos());*/
    }




    //testing
    private IEnumerator LerpToPos()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, currentDest, counter);
            transform.position = positionLerp;
            counter += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        OnDestenationReached?.Invoke(currentDest);
        reachedDest = true;
    }


}
