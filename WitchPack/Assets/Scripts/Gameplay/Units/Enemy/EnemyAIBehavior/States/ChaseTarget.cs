using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : IntervalState<EnemyAI>
{
    [SerializeField] float _outOfRangeInterval;
    private bool _isOutOfRange;

    public override void Enter(EnemyAI parent)
    {
        parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, 1);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        var target = parent.CurrentTarget;
        if (target is null) return;

        if (target.Stats.Visibility == 1 || target.IsDead)
        {
            parent.SetState(typeof(ReturnToPath));
        }
        parent.Enemy.Movement.SetDestination(target.transform.position);
    }
    public override void ChangeState(EnemyAI parent)
    {
        //check more often the chance to go back to lane
        if (!_isOutOfRange)
        {
            if (_stateCheckTimer >= _stateCheckInterval)
            {
                IntervalChangeState(parent);
                _stateCheckTimer = 0;
            }
            else
            {
                _stateCheckTimer += _usingGameTime ? GAME_TIME.GameDeltaTime : Time.deltaTime;
            }
        }
        else
        {
            if (_stateCheckTimer >= _outOfRangeInterval)
            {
                IntervalChangeState(parent);
                _stateCheckTimer = 0;
            }
            else
            {
                _stateCheckTimer += _usingGameTime ? GAME_TIME.GameDeltaTime : Time.deltaTime;
            }
        }
    }
    protected override void IntervalChangeState(EnemyAI parent)
    {
        var target = parent.CurrentTarget;
        if (target is null) return;

        var rand = Random.Range(0f, 1f);
        var distance = parent.DistanceModifier * Vector3.Distance(target.transform.position, parent.Enemy.transform.position);
        //Debug.Log("Distance is: " + distance);
        if (distance > parent.Enemy.Stats.BonusRange)
        {
            //shaman is out of defined range
            _isOutOfRange = true;
        }
        else
        {
            _isOutOfRange = false;
        }

        if (rand < parent.ReturnChance)
        {
            parent.Enemy.Movement.Agent.stoppingDistance = 0;
            parent.SetState(typeof(ReturnToPath));
        }
    }

    public override void Exit(EnemyAI parent)
    {
        if (parent.CurrentTarget != null)
        {
            parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, -1);
            parent.SetTarget(null);
        }
        base.Exit(parent);
    }
}