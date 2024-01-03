using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoroutineState : MonoBehaviour
{
    protected BaseStateHandler handler;
    public int priority;

    public void CacheHandler(BaseStateHandler handler)
    {
        this.handler = handler;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract IEnumerator RunState();


    public abstract bool IsLegal();
}
