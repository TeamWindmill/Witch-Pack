using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private DamageNumber _numberPrefab;

    public DamageNumber NumberPrefab { get => _numberPrefab; }

    public void SpawnDamagePopup(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        Color numberPopupColor = Color.white;
        if (isCrit)
        {
            numberPopupColor = Color.yellow;
        }
        NumberPrefab.Spawn(damageable.Owner.transform.position, damage.GetFinalDamage(), numberPopupColor);
    }
}
