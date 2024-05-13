using System;

public class AutoAttackCaster : ICaster
{
    public event Action OnAttack;
    public CastingAbilitySO AbilitySo => abilitySo;
    public float LastCast { get; set; }

    private readonly BaseUnit _unit;
    private readonly CastingAbilitySO abilitySo;

    public AutoAttackCaster(BaseUnit owner, CastingAbilitySO abilitySo)
    {
        _unit = owner;
        this.abilitySo = abilitySo;
    }

    public bool CastAbility()
    {
        if (abilitySo.CastAbility(_unit))
        {
            OnAttack?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetCooldown() => 1 / _unit.Stats.AttackSpeed;
    public bool CheckCastAvailable()
    {
        return abilitySo.CheckCastAvailable(_unit);
    }

    public bool ContainsUpgrade(ICaster caster)
    {
        return false;
    }
}
