using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetHandler
{
    private int jumpsLeft;
    private TargetData targetData;
    private float jumpsRange;
    private TargetedShot refShot;
    private float speed;

    public RicochetHandler(TargetedShot shot/*thisll be a generic shot once I get to it*/, int numberOfJumps, TargetData targetData, float jumpsRange, float ricochetSpeed)
    {
        jumpsLeft = numberOfJumps;
        this.jumpsRange = jumpsRange;
        this.targetData = targetData;
        this.refShot = shot;
        this.speed = ricochetSpeed;
        shot.SetRemainActive();
        shot.OnShotHit.AddListener(JumpToTarget);
    }

    public void JumpToTarget(BaseAbility ability, BaseUnit shooter, BaseUnit target)
    {
        if (jumpsLeft <= 0)
        {
            return;
        }
        BaseUnit targat = shooter.TargetHelper.GetTarget(shooter.Targeter.GetAvailableTargets(target, jumpsRange), targetData);
        if (ReferenceEquals(targat, null))//if no target was found
        {
            refShot.Disable();
            return;
        }
        Vector2 dir = targat.transform.position - target.transform.position;
        refShot.transform.position = target.transform.position;
        refShot.SetSpeed(speed);
        refShot.gameObject.SetActive(true);
        refShot.Fire(shooter, ability, dir.normalized, targat);
        jumpsLeft--;
    }


}
