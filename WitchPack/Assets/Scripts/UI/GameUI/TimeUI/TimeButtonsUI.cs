using System;
using UnityEngine;


public class TimeButtonsUI : ChangeColorToggleButton
{
public event Action<TimeButtonsUI> OnTurnOn;
[SerializeField] private float _time;

protected override void On()
{
    GAME_TIME.SetTimeStep(_time);
    OnTurnOn?.Invoke(this);
}

protected override void Off()
{
}
}