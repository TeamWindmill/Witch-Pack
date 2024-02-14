using System.Collections.Generic;
using UnityEngine;

public class RicochetHandler
{
    private int jumpsLeft;
    private TargetData targetData;
    private float jumpsRange;
    private float speed;

    public RicochetHandler(TargetedShot shot/*thisll be a generic shot once I get to it*/, int numberOfJumps, TargetData targetData, float jumpsRange, float ricochetSpeed)
    {
        jumpsLeft = numberOfJumps;
        this.jumpsRange = jumpsRange;
        this.targetData = targetData;
        this.speed = ricochetSpeed;
        shot.OnShotHit.AddListener(JumpToTarget);
    }

    public void JumpToTarget(BaseAbility ability, BaseUnit shooter, BaseUnit originalTarget)
    {

        TargetedShot shot = LevelManager.Instance.PoolManager.ArchedShotPool.GetPooledObject();
        Enemy originalEnemy = originalTarget as Enemy;
        List<Enemy> enemies = shooter.EnemyTargeter.GetAvailableTargets(originalEnemy, jumpsRange);
        Enemy newTargat = shooter.EnemyTargetHelper.GetTarget(enemies, targetData);
        if (ReferenceEquals(newTargat, null) || newTargat.Damageable.CurrentHp <= 0)//if no target was found or if the target is dead
        {
            shot.Disable();
            return;
        }
        Vector2 dir = newTargat.transform.position - originalTarget.transform.position;
        shot.transform.position = originalTarget.transform.position;
        shot.SetSpeed(speed);
        shot.gameObject.SetActive(true);
        shot.Fire(shooter, ability, dir.normalized, newTargat);
        jumpsLeft--;
        if (jumpsLeft > 0)
        {
            shot.OnShotHit.AddListener(JumpToTarget);
            Debug.Log("jumped");
        }
       /* else
        {
            shot.Disable();
            Debug.Log("disabled");
            return;
        }*/
    }


}
