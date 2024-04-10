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
        _coreTemple.Damageable.OnGetHit += UpdateCoreHealth;
    }

    private void UpdateCoreHealth(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        UpdateUIData(_coreTemple.Damageable.CurrentHp);
    }

    public override void Hide()
    {
        _coreTemple.Damageable.OnGetHit -= UpdateCoreHealth;
    }
}