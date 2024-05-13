
using System;

public class DotTimer : Timer
{
    public event Action<DamageDealer, DamageHandler, BaseAbilitySO, bool> OnDotTick;

    private DamageDealer damageDealer;
    private int damageAmount;
    DamageHandler damage;
    private BaseAbilitySO abilitySo;
    private bool isCrit;

    public DotTimer(TimerData timerData, Action<DamageDealer, DamageHandler, BaseAbilitySO, bool> onDot, DamageDealer damageDealer, int damageAmount, BaseAbilitySO abilitySo, bool isCrit) : base(timerData)
    {
        this.damageDealer = damageDealer;
        this.damageAmount = damageAmount;
        this.abilitySo = abilitySo;
        this.isCrit = isCrit;
        OnDotTick += onDot;
    }
    protected override void OnTimerTick()
    {
        damage = new DamageHandler(damageAmount);
        OnDotTick?.Invoke(damageDealer, damage, abilitySo, isCrit);
    }

}

public struct DamageData
{
    private DamageDealer damageDealer;
    private DamageHandler damage;
    private BaseAbilitySO abilitySo;
    private bool isCrit;

    public DamageDealer DamageDealer { get => damageDealer; }
    public DamageHandler Damage { get => damage; }
    public BaseAbilitySO AbilitySo { get => abilitySo; }
    public bool IsCrit { get => isCrit; }

    public DamageData(DamageDealer damageDealer, DamageHandler damage, BaseAbilitySO abilitySo, bool isCrit)
    {
        this.damageDealer = damageDealer;
        this.damage = damage;
        this.abilitySo = abilitySo;
        this.isCrit = isCrit;
    }

}