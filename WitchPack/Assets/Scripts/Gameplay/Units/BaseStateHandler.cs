using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateHandler : MonoBehaviour
{
    [SerializeField] private List<CoroutineState> states = new List<CoroutineState>();
    [SerializeField] private BaseUnit refUnit;
    [SerializeField] private float stateDelay;
    private CoroutineState activeState;
    private bool stunned;

    public BaseUnit RefUnit { get => refUnit; }
    public bool Stunned { get => stunned; set => stunned = value; }

    private void Start()
    {
        stunned = false;
        SortStates();
        SubscribeHandler();
        //StartCoroutine(RunStateMachine());
    }

    //need to start enemies only when the player is within some king of range from them
    private IEnumerator RunStateMachine()
    {
        yield return new WaitForSeconds(1f);
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(stateDelay);
            yield return new WaitUntil(() => !stunned);
            if (!ReferenceEquals(activeState, null))
            {
                StopCoroutine(activeState.RunState());
                activeState.OnStateExit();
            }
            activeState = GetNextState();
            activeState.OnStateEnter();
            yield return StartCoroutine(activeState.RunState());
        }
    }
    private void SortStates()
    {
        states.Sort((p1, p2) => p1.priority.CompareTo(p2.priority));
    }
    private void SubscribeHandler()
    {
        foreach (var item in states)
        {
            item.CacheHandler(this);
        }
    }

    private CoroutineState GetNextState()
    {
        foreach (var item in states)
        {
            if (item.IsLegal())
            {
                return item;
            }
        }
        return null;
    }



}
