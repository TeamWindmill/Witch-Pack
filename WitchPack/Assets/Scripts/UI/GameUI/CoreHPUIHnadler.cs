using System;
using TMPro;
using UnityEngine;


public class CoreHPUIHnadler : CounterUIElement
{
    private void Start()
    {
        //Init();
        //init with core hp
    }

    public override void Init(int maxValue, int currentValue = -1)
    {
        base.Init(maxValue, currentValue);
    }

    public override void UpdateVisual()
    {
        //UpdateUiData when health changes
    }

    public override void Hide()
    {
        //unsubscribe to health change
    }
}