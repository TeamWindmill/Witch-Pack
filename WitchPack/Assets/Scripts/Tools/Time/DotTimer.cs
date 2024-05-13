
using System;

public class DotTimer : Timer
{
    public event Action<DamageDealer, DamageHandler, AbilitySO, bool> OnDotTick;

    private DamageDealer damageDealer;
    private int damageAmount;
    DamageHandler damage;
    private AbilitySO abilitySo;
    private bool isCrit;

    public DotTimer(TimerData timerData, Action<DamageDealer, DamageHandler, AbilitySO, bool> onDot, DamageDealer damageDealer, int damageAmount, AbilitySO abilitySo, bool isCrit) : base(timerData)
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
    private AbilitySO abilitySo;
    private bool isCrit;

    public DamageDealer DamageDealer { get => damageDealer; }
    public DamageHandler Damage { get => damage; }
    public AbilitySO AbilitySo { get => abilitySo; }
    public bool IsCrit { get => isCrit; }

    public DamageData(DamageDealer damageDealer, DamageHandler damage, AbilitySO abilitySo, bool isCrit)
    {
        this.damageDealer = damageDealer;
        this.damage = damage;
        this.abilitySo = abilitySo;
        this.isCrit = isCrit;
    }

}