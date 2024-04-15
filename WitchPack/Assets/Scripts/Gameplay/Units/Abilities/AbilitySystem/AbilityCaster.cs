using System;
using System.Linq;

public class AbilityCaster : ICaster
{
    
    public event Action<AbilityCaster> OnCast;
    public event Action<CastingAbility> OnCastGFX;
    public CastingAbility Ability { get => _ability; }
    public float LastCast { get; private set; }
    
    private readonly BaseUnit _unit;
    private readonly CastingAbility _ability;

    public AbilityCaster(BaseUnit owner, CastingAbility ability)
    {
        _unit = owner;
        _ability = ability;
        if (owner is Shaman shaman)
        {
            if(ability.HasSfx) OnCastGFX += shaman.ShamanAbilityCastSFX;
            if (ability.GivesEnergyPoints)
            {
                OnCast += shaman.EnergyHandler.OnShamanCast;
            }
        }
        else if (owner is Enemy enemy)
        {
            if(ability.HasSfx) OnCastGFX += enemy.AbilityCastSFX;
        }
    }

    public bool CastAbility()
    {
        if (_ability.CastAbility(_unit))
        {
            LastCast = GAME_TIME.GameTime;
            OnCast?.Invoke(this);
            OnCastGFX?.Invoke(_ability);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCastAvailable()
    {
        return _ability.CheckCastAvailable(_unit);
    }

    public bool ContainsUpgrade(ICaster caster)
    {
        return _ability.Upgrades.Contains(caster.Ability);
    }

    public float GetCooldown() => _ability.Cd;

}

