using System;
using System.Linq;

public class AbilityCaster : ICaster
{
    public event Action<AbilityCaster> OnCast;
    public event Action<CastingAbilitySO> OnCastGFX;
    public CastingAbilitySO AbilitySo => abilitySo;
    public float LastCast { get; private set; }
    
    private readonly BaseUnit _unit;
    private readonly CastingAbilitySO abilitySo;

    public AbilityCaster(BaseUnit owner, CastingAbilitySO abilitySo)
    {
        _unit = owner;
        this.abilitySo = abilitySo;
        //ability.AddStatUpgrade();
        if (owner is Shaman shaman)
        {
            if(abilitySo.HasSfx) OnCastGFX += shaman.ShamanAbilityCastSFX;
            if (abilitySo.GivesEnergyPoints)
            {
                OnCast += shaman.EnergyHandler.OnShamanCast;
            }
        }
        else if (owner is Enemy enemy)
        {
            if(abilitySo.HasSfx) OnCastGFX += enemy.AbilityCastSFX;
        }
    }

    public bool CastAbility()
    {
        if (abilitySo.CastAbility(_unit))
        {
            LastCast = GAME_TIME.GameTime;
            OnCast?.Invoke(this);
            OnCastGFX?.Invoke(abilitySo);
            return true;
        }
        
        return false;
    }

    public bool CheckCastAvailable()
    {
        return abilitySo.CheckCastAvailable(_unit);
    }

    public bool ContainsUpgrade(ICaster caster)
    {
        return abilitySo.Upgrades.Contains(caster.AbilitySo);
    }

    public float GetCooldown() => abilitySo.Cd;

}

