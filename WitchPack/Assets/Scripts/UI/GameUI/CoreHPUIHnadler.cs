using System;
using TMPro;
using UnityEngine;


public class CoreHPUIHnadler : CounterUIElement
{
    public override void Init()
    {
        base.Init(); 
        //ElementInit(); //get core hp
    }

    public override void ElementInit(int maxValue, int currentValue = -1)
    {
        base.ElementInit(maxValue, currentValue);
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