using System;
using System.Collections;
using UnityEngine;

public class UnitCastingHandler
{
    //manage cds, send abilities to cast -> specific interactions come from the so itself? 
    public event Action<UnitCastingHandler> OnCast;
    public event Action<SoundEffectType> OnCastGFX;

    private Shaman shaman;
    private BaseAbility ability;
    private float lastCast;
    private float startedCasting;

    public BaseAbility Ability { get => ability; }
    public float LastCast { get => lastCast; }
    public Shaman Shaman { get => shaman; }

    public UnitCastingHandler(BaseUnit owner, BaseAbility ability)
    {
        shaman = owner as Shaman;
        this.ability = ability;
        lastCast = GetAbilityCD() * -1;
        if(ability.HasSfx) OnCastGFX += shaman.ShamanCastSFX;
        if (ability.GivesEnergyPoints)
        {
            OnCast += shaman.EnergyHandler.OnShamanCast;
        }
    }

    public float GetAbilityCD()
    {
        return ability.Cd * (1 - shaman.Stats.AbilityCooldownReduction / 100);
    }

    public void CastAbility()
    {
        if (GAME_TIME.GameTime - lastCast >= GetAbilityCD())
        {
            if (ability.CheckCastAvailable(Shaman))
            {
                shaman.StartCoroutine(TryCastingAbility());
            }
        }
    }

    private IEnumerator TryCastingAbility()
    {
        shaman.AutoAttacker.CanAttack = false;
        float counter = 0f;
        while (counter < ability.CastTime)
        {
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (!shaman.AutoAttacker.CanAttack)//changes when casting succesfuly or when reaching a new destenation
        {
            if (ability.CastAbility(shaman))
            {
                lastCast = GAME_TIME.GameTime;
                OnCast?.Invoke(this);
                OnCastGFX?.Invoke(ability.SoundEffectType);
                shaman.AutoAttacker.CanAttack = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

}

