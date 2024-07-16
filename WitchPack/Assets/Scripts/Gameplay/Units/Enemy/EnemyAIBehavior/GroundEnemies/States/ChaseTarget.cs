using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : IntervalState<EnemyAI>
{
    [SerializeField,Tooltip("a different interval for changing state when the enemy's target is out of range")] float _outOfRangeInterval;
    [SerializeField,Tooltip("a modifier added to the enemy base range that enables outOfRangeInterval state checks")] float _distanceModifier;
    [SerializeField,Range(0,1)] float _returnChance;
    private bool _isOutOfRange;

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        parent.Enemy.AutoCaster.EnableCaster();
        parent.Enemy.EnemyVisualHandler.EnemyEffectHandler.PlayEffect(StatusEffectVisual.Taunt);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null) return;

        if (target.Stats[StatType.Visibility].IntValue == 1 || target.IsDead)
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
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null) return;

        var rand = Random.Range(0f, 1f);
        var distance = _distanceModifier * Vector3.Distance(target.transform.position, parent.Enemy.transform.position);
        //Debug.Log("Distance is: " + distance);
        if (distance > parent.Enemy.Stats[StatType.BaseRange].Value)
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
        parent.Enemy.AutoCaster.DisableCaster();
        parent.Enemy.EnemyVisualHandler.EnemyEffectHandler.DisableEffect(StatusEffectVisual.Taunt);
        base.Exit(parent);
    }
}