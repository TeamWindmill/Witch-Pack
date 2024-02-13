
using System;

public class DotTimer : Timer
{
    public event Action<DamageOverTimeData> OnDotTick;

    private readonly DamageOverTimeData _dotData;
    public DotTimer(TimerData timerData,DamageOverTimeData dotData) : base(timerData) => _dotData = dotData;

    protected override void OnTimerTick() => OnDotTick?.Invoke(_dotData);
}

public struct DamageOverTimeData
{
    private DamageDealer dealer;
    private DamageHandler damage;
    private BaseAbility ability;
    private bool isCrit;
}