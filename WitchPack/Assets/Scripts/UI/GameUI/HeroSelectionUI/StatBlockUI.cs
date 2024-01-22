using System;
using TMPro;
using UnityEngine;

public class StatBlockUI : MonoBehaviour
{
    [SerializeField] private StatType statTypeId;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _statValue;
    
    private Color _statBonusAdditionColor;
    private Color _statBonusReductionColor;

    private float _baseValue;
    public StatType StatTypeId => statTypeId;
    
    public void Init(float currentValue, Color addColor, Color reduceColor)
    {
        _baseValue = currentValue;
        _statBonusAdditionColor = addColor;
        _statBonusReductionColor = reduceColor;
        SetStatText(_baseValue,0);
    }
    public void UpdateUI(int newValue)
    {
        if(!HeroSelectionUI.Instance.IsActive) return;
        SetStatText(_baseValue,newValue);
    }

    private void SetStatText(float baseValue, int bonusValue)
    {
        string statName = "";
        string modifier = "";
        switch (statTypeId)
        {
            case StatType.BaseDamage:
                statName = "Damage";
                break;
            case StatType.AttackSpeed:
                statName = "Attack Speed";
                break;
            case StatType.BaseRange:
                statName = "Range";
                break;
            case StatType.MovementSpeed:
                statName = "Move Speed";
                break;
            case StatType.CritDamage:
                statName = "Crit Damage";
                baseValue += 100;
                modifier = "%";
                break;
            case StatType.CritChance:
                statName = "Crit Chance";
                modifier = "%";
                break;
        }
    
        _statText.text = statName;
        string modifierText = $"{modifier}";
        string baseValueText = $"{baseValue}";
        string bonusValueText;
        switch (bonusValue)
        {
            case > 0:
                bonusValueText = ColorLogHelper.SetColorToString($" (+{bonusValue})", _statBonusAdditionColor);;
                _statValue.text =  baseValueText + modifierText + bonusValueText;
                break;
            case < 0:
                bonusValueText = ColorLogHelper.SetColorToString($" (-{-bonusValue})", _statBonusReductionColor);;
                _statValue.text = baseValueText + modifierText + bonusValueText;
                break;
            case 0:
                _statValue.text = baseValueText + modifierText;
                break;
        }
    }
}
