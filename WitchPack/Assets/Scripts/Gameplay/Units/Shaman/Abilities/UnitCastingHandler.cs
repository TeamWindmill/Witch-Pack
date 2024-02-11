using System;
using System.Collections;
using UnityEngine;

public class UnitCastingHandler
{
    //manage cds, send abilities to cast -> specific interactions come from the so itself? 
    public event Action<UnitCastingHandler> OnCast;

    private BaseUnit unit;
    private BaseAbility ability;
    private float lastCast;
    private float startedCasting;

    public BaseAbility Ability { get => ability; }
    public float LastCast { get => lastCast; }
    public BaseUnit Unit { get => unit; }

    public UnitCastingHandler(BaseUnit owner, BaseAbility ability)
    {
        unit = owner;
        this.ability = ability;
        lastCast = GetAbilityCD() * -1;
        if (ability.GivesEnergyPoints)
        {
            var shaman = owner as Shaman;
            OnCast += shaman.EnergyHandler.OnShamanCast;
        }
    }

    public float GetAbilityCD()
    {
        return ability.Cd * (1 - unit.Stats.AbilityCooldownReduction / 100);
    }

    public void CastAbility()
    {
        if (GAME_TIME.GameTime - lastCast >= GetAbilityCD())
        {
            if (ability.CheckCastAvailable(Unit))
            {
                unit.StartCoroutine(TryCastingAbility());
            }
        }
    }

    private IEnumerator TryCastingAbility()
    {
        unit.AutoAttacker.CanAttack = false;
        float counter = 0f;
        while (counter < ability.CastTime)
        {
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (!unit.AutoAttacker.CanAttack)//changes when casting succesfuly or when reaching a new destenation
        {
            if (ability.CastAbility(unit))
            {
                lastCast = GAME_TIME.GameTime;
                OnCast?.Invoke(this);
                unit.AutoAttacker.CanAttack = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

}

