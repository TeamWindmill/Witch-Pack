using System;
using UnityEngine;

public class UnitCastingHandler
{
    //manage cds, send abilities to cast -> specific interactions come from the so itself? 
    public event Action<UnitCastingHandler> OnCast;

    private BaseUnit unit;
    private BaseAbility ability;
    private float lastCast;

    public BaseAbility Ability { get => ability; }
    public float LastCast { get => lastCast; }
    public BaseUnit Unit { get => unit; }

    public UnitCastingHandler(BaseUnit owner, BaseAbility ability)
    {
        unit = owner;
        this.ability = ability;
        lastCast = GetAbilityCD() * -1;
    }

    public float GetAbilityCD()
    {
        return ability.Cd * (1 - unit.Stats.AbilityCooldownReduction / 100);
    }

    public void CastAbility()
    {
        if (GAME_TIME.GameTime - lastCast >= GetAbilityCD())
        {
            if (ability.CastAbility(unit))
            {
                lastCast = GAME_TIME.GameTime;
                OnCast?.Invoke(this);
            }
        }
    }



}
