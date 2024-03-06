using System;

public class AutoAttackHandler : ICaster
{
    public event Action OnAttack;
    public CastingAbility Ability => _ability;
    public float LastCast { get; set; }

    private readonly BaseUnit _unit;
    private readonly CastingAbility _ability;

    public AutoAttackHandler(BaseUnit owner, CastingAbility ability)
    {
        _unit = owner;
        _ability = ability;
    }

    public bool CastAbility()
    {
        if (_ability.CastAbility(_unit))
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
        return _ability.CheckCastAvailable(_unit);
    }
}
