using System;
using TMPro;
using UnityEngine;


public class CoreHPUIHnadler : CounterUIElement
{
    private CoreTemple _coreTemple;
    public override void Init()
    {
        _coreTemple = LevelManager.Instance.CurrentLevel.CoreTemple;
        ElementInit(_coreTemple.MaxHp,_coreTemple.CurHp);
        _coreTemple.OnGetHit += UpdateCoreHealth;
    }

    private void UpdateCoreHealth(int obj)
    {
        UpdateUIData(_coreTemple.CurHp);
    }

    public override void Hide()
    {
        _coreTemple.OnGetHit -= UpdateCoreHealth;
    }
}