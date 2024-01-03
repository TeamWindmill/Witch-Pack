using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CoroutineState
{
    public override bool IsLegal()
    {
        if ((handler.RefUnit as Shaman).EnemyTargeter.AvailableTargets.Count >0 /*&& reached dest*/)
        {
            return true;
        }
        return false;
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
