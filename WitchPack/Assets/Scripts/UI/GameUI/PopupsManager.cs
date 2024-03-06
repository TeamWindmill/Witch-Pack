using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private DamageNumber popupPrefab;
    [SerializeField] private DamageNumber levelUpPopupPrefab;
    [SerializeField] private DamageNumber healPopupPrefab;
    private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _sinSpeed;
    [Range(1, 100), SerializeField] private float _offsetMultiplier;
    [SerializeField] private float _levelUpSpawnDelay;
    private float _offsetDivider = 10;
    private Vector3 _offsetVector;

    private Color _popupColor;

    string _popupText;

    [SerializeField] private StatusEffectTypeColorDictionary _dictionary;
    public DamageNumber PopupPrefab { get => popupPrefab; }

    private void Update()
    {
        _xOffset = Mathf.Sin(_sinSpeed * GAME_TIME.GameTime) * (_offsetMultiplier / _offsetDivider);
    }

    public void SpawnDamagePopup(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        _popupText = "";
        _offsetVector = new Vector3(_xOffset, _yOffset);

        _popupColor = Color.white;
        if (isCrit)
        {
            _popupColor = Color.red;
        }
        else if(damage.HasPopupColor)
        {
            _popupColor = damage.PopupColor;
        }
        else if(ability.HasPopupColor)
        {
            _popupColor = ability.PopupColor;
        }
        

        _popupText = damage.GetFinalDamage().ToString();
        PopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, _popupText, _popupColor);
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

    public void SpawnHealPopup(Damageable damageable, float healAmount)
    {
        _popupText = healAmount.ToString();
        _offsetVector = new Vector3(0, _yOffset);

        healPopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, _popupText);
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
}
