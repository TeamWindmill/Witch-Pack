using TMPro;
using UnityEngine;

public class StatBlockUI : MonoBehaviour
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
    
    public void Init(Stat stat, Color addColor, Color reduceColor)
    {
        _stat = stat;
        _baseValue = stat.Value;
        stat.OnStatChange += UpdateBaseStat;
        _statBonusAdditionColor = addColor;
        _statBonusReductionColor = reduceColor;
        SetStatText(_baseValue,0);
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
        if(_stat != null) _stat.OnStatChange -= UpdateBaseStat;
    }

    public void UpdateBaseStat(float newValue)
    {
        if(ReferenceEquals(HeroSelectionUI.Instance,null)) return;
        if(!HeroSelectionUI.Instance.IsActive) return;
        _baseValue = newValue;
        SetStatText(_baseValue, 0);
    }

    private void SetStatText(float baseValue, float bonusValue)
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
