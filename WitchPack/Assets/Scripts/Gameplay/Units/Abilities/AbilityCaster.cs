using System;

public class AbilityCaster : ICaster
{
    
    public event Action<AbilityCaster> OnCast;
    public event Action<CastingAbility> OnCastGFX;
    public CastingAbility Ability { get => _ability; }
    public float LastCast { get; private set; }
    
    private readonly Shaman _shaman;
    private readonly CastingAbility _ability;

    public AbilityCaster(BaseUnit owner, CastingAbility ability)
    {
        if (owner is Shaman shaman)
        {
            _shaman = shaman;
            _ability = ability;
            
            if(ability.HasSfx) OnCastGFX += _shaman.ShamanCastGfx;
            if (ability.GivesEnergyPoints)
            {
                OnCast += _shaman.EnergyHandler.OnShamanCast;
            }
        }
    }

    public bool CastAbility()
    {
        if (_ability.CastAbility(_shaman))
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
        return _ability.CheckCastAvailable(_shaman);
    }

    public float GetCooldown() => _ability.Cd;

}

