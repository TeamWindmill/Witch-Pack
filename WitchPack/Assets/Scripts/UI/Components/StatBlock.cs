using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public abstract class StatBlock<T> : UIElement where T : Enum
{
    [SerializeField] protected T statTypeId;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _statValue;
    
    private Color _statBonusAdditionColor;
    private Color _statBonusReductionColor;

    private BaseStat<T> _stat;
    private float _baseValue;
    private float _bonusValue;
    public T StatTypeId => statTypeId;
    
    public void Init(BaseStat<T> stat, Color addColor = default, Color reduceColor = default)
    {
        if(stat is null) return;
        _stat = stat;
        _baseValue = stat.Value;
        stat.OnStatChange += UpdateBaseStat;
        _statBonusAdditionColor = addColor;
        _statBonusReductionColor = reduceColor;
        SetStatText(_baseValue,0);
    }

    public void SetStatType(T statType)
    {
        statTypeId = statType;
    }

    public override void Hide()
    {
        base.Hide();
        if(_stat is null) return;
        _stat.OnStatChange -= UpdateBaseStat;
        _stat = null;
    }

    public void UpdateBonusStatUI(float newValue)
    {
        if(ReferenceEquals(HeroSelectionUI.Instance,null)) return;
        if(!HeroSelectionUI.Instance.IsActive) return;
        _bonusValue = newValue;
        SetStatText(_baseValue,_bonusValue);
    }

    public void HideBonusStatUI()
    {
        SetStatText(_baseValue,0);
    }

    public void UpdateBaseStat(float newValue)
    {
        if(ReferenceEquals(_stat,null)) return;
        _baseValue = newValue;
        SetStatText(_baseValue, 0);
    }

    private void SetStatText(float baseValue, float bonusValue)
    {
        string statName = "";
        string modifier = "";
        statName = GetStatName(ref baseValue, statName, ref modifier);
    
        _statText.text = statName;
        string modifierText = $"{modifier}";
        string baseValueText = $"{baseValue}";
        string bonusValueText;
        if (bonusValue == 0)
        {
            _statValue.text = baseValueText + modifierText;
            return;
        }
        switch (bonusValue)
        {
            case > 0.1f:
                bonusValueText = ColorLogHelper.SetColorToString($" (+{bonusValue.ToString("F1")})", _statBonusAdditionColor);;
                _statValue.text =  baseValueText + modifierText + bonusValueText;
                break;
            case < -0.1f:
                bonusValueText = ColorLogHelper.SetColorToString($" (-{math.abs(bonusValue).ToString("F1")})", _statBonusReductionColor);;
                _statValue.text = baseValueText + modifierText + bonusValueText;
                break;
            default:
                _statValue.text = baseValueText + modifierText;
                break;
        }
    }

    protected abstract string GetStatName(ref float baseValue, string statName, ref string modifier);
}