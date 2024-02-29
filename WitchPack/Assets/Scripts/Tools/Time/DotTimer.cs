
using System;

public class DotTimer : Timer
{
    public event Action<DamageDealer, DamageHandler, BaseAbility, bool> OnDotTick;

    private DamageDealer damageDealer;
    private int damageAmount;
    DamageHandler damage;
    private BaseAbility ability;
    private bool isCrit;

    public DotTimer(TimerData timerData, Action<DamageDealer, DamageHandler, BaseAbility, bool> onDot, DamageDealer damageDealer, int damageAmount, BaseAbility ability, bool isCrit) : base(timerData)
    {
        this.damageDealer = damageDealer;
        this.damageAmount = damageAmount;
        this.ability = ability;
        this.isCrit = isCrit;
        OnDotTick += onDot;
    }
    protected override void OnTimerTick()
    {
        damage = new DamageHandler(damageAmount);
        OnDotTick?.Invoke(damageDealer, damage, ability, isCrit);
    }

}

public struct DamageData
{
    private DamageDealer damageDealer;
    private DamageHandler damage;
    private BaseAbility ability;
    private bool isCrit;

    public DamageDealer DamageDealer { get => damageDealer; }
    public DamageHandler Damage { get => damage; }
    public BaseAbility Ability { get => ability; }
    public bool IsCrit { get => isCrit; }

    public DamageData(DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        this.damageDealer = damageDealer;
        this.damage = damage;
        this.ability = ability;
        this.isCrit = isCrit;
    }

}