using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private DamageNumber popupPrefab;
    private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _sinSpeed;
    [Range(1, 100), SerializeField] private float _offsetMultiplier;
    private float _offsetDivider = 10;
    private Vector3 _offsetVector;

    private Color _popupColor;

    string _statusEffectText;

    [SerializeField] private StatusEffectTypeColorDictionary _dictionary;
    public DamageNumber PopupPrefab { get => popupPrefab; }

    private void Update()
    {
        _xOffset = Mathf.Sin(_sinSpeed * GAME_TIME.GameTime) * (_offsetMultiplier / _offsetDivider);
    }

    public void SpawnDamagePopup(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        _statusEffectText = "";
        _offsetVector = new Vector3(_xOffset, _yOffset);
        _popupColor = Color.white;
        if (isCrit)
        {
            _popupColor = Color.red;
        }
        _statusEffectText = damage.GetFinalDamage().ToString();
        PopupPrefab.Spawn(damageable.Owner.transform.position + _offsetVector, _statusEffectText, _popupColor);
    }

    public void SpawnStatusEffectPopup(Effectable effectable, Affector affector, StatusEffect statusEffect)
    {
        _statusEffectText = "";
        _popupColor = _dictionary.GetColorByStatusEffectType(statusEffect.StatusEffectType);

        switch (statusEffect.StatusEffectType)
        {
            case StatusEffectType.Root:
                _statusEffectText = "Root";           
                break;

            case StatusEffectType.Slow:
                _statusEffectText = "Slow";
                break;

            case StatusEffectType.Charm:
                _statusEffectText = "Charm";
                break;

            default:
                return;
        }

        PopupPrefab.Spawn(effectable.Owner.transform.position, _statusEffectText, _popupColor);
    }

    public void SpawnTextPopup()
    {
        
    }
}
