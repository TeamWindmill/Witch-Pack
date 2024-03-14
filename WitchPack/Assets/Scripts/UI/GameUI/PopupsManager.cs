using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;
using System;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private DamageNumber textPopupPrefab;
    [SerializeField] private DamageNumber levelUpPopupPrefab;
    [SerializeField] private DamageNumber damagePopupPrefab;
    [SerializeField] private DamageNumber bigDamagePopupPrefab;
    [SerializeField] private DamageNumber healPopupPrefab;
    private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _sinSpeed;
    [Range(1, 100), SerializeField] private float _offsetMultiplier;
    [SerializeField] private float _levelUpSpawnDelay;
    private float _offsetDivider = 10;
    private Vector3 _offsetVector;

    [SerializeField] private Color shamanCritColor;
    [SerializeField] private Color enemyAutoAttackColor;
    [SerializeField] private Color enemyCritAutoAttackColor;
    [SerializeField] private Color healColor;
    [SerializeField] private StatusEffectTypeColorDictionary _dictionary;

    private Color _popupColor;

    string _popupText;
    float _popupNumber;

    float maxDamageReached;

    public DamageNumber PopupPrefab { get => textPopupPrefab; }

    private void Update()
    {
        _xOffset = Mathf.Sin(_sinSpeed * GAME_TIME.GameTime) * (_offsetMultiplier / _offsetDivider);
    }

    public void SpawnDamagePopup(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        _offsetVector = new Vector3(_xOffset, _yOffset);

        _popupColor = DetermineDamagePopupColor(damageDealer, damage, ability, isCrit);

         _popupNumber = damage.GetFinalDamage();
        if(isCrit || damageDealer.Owner is Enemy)
        {
            bigDamagePopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, _popupNumber, _popupColor);
        }
        else
        {
            damagePopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, _popupNumber, _popupColor);
        }
    }

    public void SpawnStatusEffectPopup(Effectable effectable, Affector affector, StatusEffect statusEffect)
    {
        if(statusEffect.StatusEffectType == StatusEffectType.None) return;

        _offsetVector = new Vector3(0, _yOffset);

        StatusEffectTypeVisualData data = _dictionary.GetData(statusEffect.StatusEffectType);
        _popupColor = data.Color;

        _popupText = "";
        _popupText = data.Name;

        PopupPrefab.Spawn(effectable.Owner.transform.position + _offsetVector, _popupText, _popupColor);
    }

    public void SpawnGeneralPopup(string text, Color color, Vector3 position, bool xOffset = false, bool yOffset = false)
    {
        _offsetVector = new Vector3(0, 0);
        if(xOffset)
        {
            _offsetVector.x += _xOffset;
        }
        if(yOffset)
        {
            _offsetVector.y += _yOffset;
        }

        PopupPrefab.Spawn(position + _offsetVector, text, color);
    }

    public void SpawnHealPopup(Damageable damageable, float healAmount)
    {
        _offsetVector = new Vector3(0, _yOffset);

        healPopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, healAmount, healColor);
    }

    public void SpawnLevelUpTextPopup(Shaman shaman)
    {
        StartCoroutine(LevelUpSpawnDelay(shaman));
    }

    private IEnumerator LevelUpSpawnDelay(Shaman shaman)
    {
        yield return new WaitForSeconds(_levelUpSpawnDelay);
        _offsetVector = new Vector3(0, _yOffset, 0);
        levelUpPopupPrefab.Spawn(shaman.transform.position + _offsetVector);
    }

    private Color DetermineDamagePopupColor(DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        // First Priority - Specific Damage Color
        if (damage.HasPopupColor)
        {
            return damage.PopupColor;
        }
        // Second Priority - Specific Ability Color
        if (ability.HasPopupColor)
        {
            return ability.PopupColor;
        }

        // Third Priority - Crit Color
        if (isCrit)
        {
            if (damageDealer.Owner is Enemy enemy && !enemy.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
            {
                return enemyCritAutoAttackColor;
            }
            else
            {
                return shamanCritColor;
            }
        }

        // Fourth Priority - Default Colors
        else if (damageDealer.Owner is Enemy enemy && !enemy.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
        {
            return enemyAutoAttackColor;
        }
        else
        {
            return Color.white;
        }
    }
}
