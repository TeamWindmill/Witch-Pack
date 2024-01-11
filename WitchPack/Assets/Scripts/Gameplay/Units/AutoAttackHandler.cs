using System;
using UnityEngine;

public class AutoAttackHandler
{
    public Action OnAttack;

    private BaseUnit unit;
    private BaseAbility ability;
    private float lastCast;

    public BaseAbility Ability { get => ability; }
    public float LastCast { get => lastCast; }
    public BaseUnit Unit { get => unit; }
    public AutoAttackHandler(BaseUnit owner, BaseAbility ability)
    {
        unit = owner;
        this.ability = ability;
        lastCast = GetAACD() * -1;
    }
  
    public float GetAACD()
    {
        return 1 / unit.Stats.AttackSpeed;
    }

    public void Attack()
    {
        if (GAME_TIME.GameTime - lastCast >= GetAACD())
        {
            if (ability.CastAbility(unit))
            {
                lastCast = GAME_TIME.GameTime;
                OnAttack?.Invoke();
            }
        }
    }


}
