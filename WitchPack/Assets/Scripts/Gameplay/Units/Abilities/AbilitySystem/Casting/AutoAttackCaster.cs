using System;

public class AutoAttackCaster : ICaster
{
    public event Action OnAttack;
    public CastingAbility Ability => ability;
    public float LastCast { get; set; }

    private readonly BaseUnit _unit;
    private readonly CastingAbility ability;

    public AutoAttackCaster(BaseUnit owner, CastingAbility ability)
    {
        _unit = owner;
        this.ability = ability;
    }

    public bool CastAbility()
    {
        if (ability.CastAbility())
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
        return ability.CheckCastAvailable();
    }

    public bool ContainsUpgrade(ICaster caster)
    {
        return false;
    }
}
