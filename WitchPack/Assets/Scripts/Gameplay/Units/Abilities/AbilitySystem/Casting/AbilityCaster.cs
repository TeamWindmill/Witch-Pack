using System;

public class AbilityCaster : ICaster
{
    public event Action<AbilityCaster> OnCast;
    public event Action<CastingAbilitySO> OnCastGFX;
    public CastingAbility Ability => ability;
    public float LastCast { get; private set; }
    
    private readonly BaseUnit _unit;
    private readonly CastingAbility ability;

    public AbilityCaster(BaseUnit owner, CastingAbility ability)
    {
        _unit = owner;
        this.ability = ability;
        if (owner is Shaman shaman)
        {
            if(ability.CastingConfig.HasSfx) OnCastGFX += shaman.ShamanAbilityCastSFX;
            if (ability.CastingConfig.GivesEnergyPoints)
            {
                OnCast += shaman.EnergyHandler.OnShamanCast;
            }
        }
        else if (owner is Enemy enemy)
        {
            if(ability.CastingConfig.HasSfx) OnCastGFX += enemy.AbilityCastSFX;
        }
    }

    public bool CastAbility()
    {
        if (ability.CastAbility())
        {
            LastCast = GAME_TIME.GameTime;
            OnCast?.Invoke(this);
            OnCastGFX?.Invoke(ability.CastingConfig);
            return true;
        }
        
        return false;
    }
    public bool CheckCastAvailable()
    {
        return ability.CheckCastAvailable();
    }

    public bool ContainsUpgrade(ICaster caster)
    {
        return ability.GetUpgrades().Contains(caster.Ability);
    }
    public float GetCastTime() => ability.GetAbilityStatValue(AbilityStatType.CastTime);
    

    public float GetCooldown() => ability.GetAbilityStatValue(AbilityStatType.Cooldown) * _unit.Stats[StatType.AbilityCooldownReduction].Value;

}

