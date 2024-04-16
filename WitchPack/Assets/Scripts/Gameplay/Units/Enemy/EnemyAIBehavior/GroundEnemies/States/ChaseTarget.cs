using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : IntervalState<EnemyAI>
{
    [SerializeField] float _outOfRangeInterval;
    [SerializeField] float _distanceModifier;
    [SerializeField,Range(0,1)] float _returnChance;
    private bool _isOutOfRange;

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, parent.Enemy.EnemyConfig.Threat);
        parent.Enemy.AutoCaster.EnableCaster();
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
        var distance = _distanceModifier * Vector3.Distance(target.transform.position, parent.Enemy.transform.position);
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

        if (rand < _returnChance)
        {
            parent.SetState(typeof(ReturnToPath));
        }
    }

    public override void Exit(EnemyAI parent)
    {
        if (parent.CurrentTarget != null)
        {
            parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, -parent.Enemy.EnemyConfig.Threat);
            parent.SetTarget(null);
        }
        parent.Enemy.AutoCaster.DisableCaster();
        base.Exit(parent);
    }
}