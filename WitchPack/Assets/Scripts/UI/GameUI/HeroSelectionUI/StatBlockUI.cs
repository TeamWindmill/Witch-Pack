using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class StatBlockUI : UIElement
{
    [SerializeField] private StatType statTypeId;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _statValue;
    
    private Color _statBonusAdditionColor;
    private Color _statBonusReductionColor;

    private Stat _stat;
    private float _baseValue;
    private float _bonusValue;
    public StatType StatTypeId => statTypeId;
    
    public void Init(Stat stat, Color addColor = default, Color reduceColor = default)
    {
        _stat = stat;
        _baseValue = stat.Value;
        stat.OnStatChange += UpdateBaseStat;
        _statBonusAdditionColor = addColor;
        _statBonusReductionColor = reduceColor;
        SetStatText(_baseValue,0);
    }

    public override void Hide()
    {
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
        switch (statTypeId)
        {
            case StatType.MaxHp:
                statName = "Health";
                break;
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
            case StatType.HpRegen:
                statName = "Regeneration";
                break;
            case StatType.Armor:
                statName = "Armor";
                break;
            case StatType.AbilityCooldownReduction:
                statName = "Armor";
                break;
        }
    
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
}
