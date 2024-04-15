using System;
using TMPro;
using UnityEngine;


public class CoreHPUIHandler : StatBarUIElement
{
    private CoreTemple _coreTemple;
    public override void Init()
    {
        _coreTemple = LevelManager.Instance.CurrentLevel.CoreTemple;
        ElementInit(_coreTemple.Damageable.MaxHp);
        _coreTemple.Damageable.OnTakeDamage += (value) => UpdateUIData(_coreTemple.Damageable.CurrentHp);
        _coreTemple.Damageable.OnHeal += (damageable, value) => UpdateUIData(_coreTemple.Damageable.CurrentHp);
    }

    public override void Hide()
    {
        _coreTemple.Damageable.OnTakeDamage -= (value) => UpdateUIData(_coreTemple.Damageable.CurrentHp);
        _coreTemple.Damageable.OnHeal -= (damageable, value) => UpdateUIData(_coreTemple.Damageable.CurrentHp);
    }
}