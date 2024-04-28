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
        _coreTemple.Damageable.OnTakeDamage += UpdateUIOnDamage;
        _coreTemple.Damageable.OnHeal += UpdateUIOnHeal;
    }

    public override void Hide()
    {
        _coreTemple.Damageable.OnTakeDamage -= UpdateUIOnDamage;
        _coreTemple.Damageable.OnHeal -= UpdateUIOnHeal;
    }
    private void UpdateUIOnDamage(int value)
    {
        UpdateUIData(_coreTemple.Damageable.CurrentHp);
    }
    private void UpdateUIOnHeal(Damageable damageable, float f)
    {
        UpdateUIData(_coreTemple.Damageable.CurrentHp);
    }
}