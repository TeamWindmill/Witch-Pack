using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CoroutineState
{
    public override bool IsLegal()
    {
        return true;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override IEnumerator RunState()
    {
        yield return new WaitForEndOfFrame();
    }
}
