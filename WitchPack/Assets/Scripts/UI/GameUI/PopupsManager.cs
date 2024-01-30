using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private DamageNumber _numberPrefab;
    private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _sinSpeed;
    [Range(1, 100), SerializeField] private float _offsetMultiplier;
    private float _offsetDivider = 10;
    private Vector3 _offsetVector;

    

    private void Update()
    {
        _xOffset = Mathf.Sin(_sinSpeed * GAME_TIME.GameTime) * (_offsetMultiplier / _offsetDivider);

    }

    public DamageNumber NumberPrefab { get => _numberPrefab; }

    public void SpawnDamagePopup(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        _offsetVector = new Vector3(_xOffset, _yOffset);
        Color numberPopupColor = Color.white;
        if (isCrit)
        {
            numberPopupColor = Color.yellow;
        }
        NumberPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, damage.GetFinalDamage(), numberPopupColor);
    }
}
