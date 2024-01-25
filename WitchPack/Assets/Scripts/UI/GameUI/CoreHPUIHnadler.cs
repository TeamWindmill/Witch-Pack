using System;
using TMPro;
using UnityEngine;


public class CoreHPUIHnadler : CounterUIElement
{
    public override void Init()
    {
        var core = LevelManager.Instance.CurrentLevel.CoreTemple;
        ElementInit(core.MaxHp,core.CurHp);
        core.OnGetHit += UpdateCoreHealth;
    }

    private void UpdateCoreHealth(int obj)
    {
        UpdateUIData(obj);
    }

    public override void Hide()
    {
        //unsubscribe to health change
    }
}