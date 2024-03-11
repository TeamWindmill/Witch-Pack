using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Charmed", fileName = "Charmed")]
public class Charmed : State<EnemyAI>
{
    [SerializeField] private TargetData _targetData;

    private BaseUnit _caster;

    public void StartCharm(BaseUnit caster, Enemy affectedTarget)
    {
        _caster = caster;
        affectedTarget.EnemyAI.SetState(typeof(Charmed));
    }

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        parent.Enemy.AutoCaster.EnableCaster();
        base.Enter(parent);
    }

    public override void UpdateState(EnemyAI parent)
    {
        var target = parent.Enemy.EnemyTargetHelper.GetTarget(_targetData);
        if (!ReferenceEquals(target, null))
        {
            parent.Enemy.Movement.SetDestination(target.transform.position);
        }
        else
        {
            parent.Enemy.Movement.SetDestination(_caster.transform.position);
        }
    }

    public override void ChangeState(EnemyAI parent)
    {
        
    }
    public void EndCharm(Effectable parent,StatusEffect statusEffect)
    {
        (parent.Owner as Enemy)?.EnemyAI.SetState(typeof(ReturnToPath));
    }

    public override void Exit(EnemyAI parent)
    {
        var enemy = parent.Enemy;
        LevelManager.Instance.CharmedEnemies.Remove(enemy);
        parent.Enemy.AutoCaster.DisableCaster();
        base.Exit(parent);
    }
}