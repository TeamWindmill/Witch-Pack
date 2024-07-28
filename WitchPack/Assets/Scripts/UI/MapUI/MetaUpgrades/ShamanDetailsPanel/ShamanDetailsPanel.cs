using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShamanDetailsPanel : UIElement<ShamanSaveData>
{
    [SerializeField] private Image _shamanSplash;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _skillPointsText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private StatBar _expBar;
    [SerializeField] private StatBlockUI[] _statBlocks;

    private UnitStats _shamanStats;
    private ShamanSaveData _shaman;
    public override void Init(ShamanSaveData rootAbility)
    {
        _shaman = rootAbility;
        _nameText.text = rootAbility.Config.Name;
        _shamanSplash.sprite = rootAbility.Config.UnitSprite;
        _descriptionText.text = rootAbility.Config.Description;
        _skillPointsText.text = "SP " + rootAbility.ShamanExperienceHandler.AvailableSkillPoints;
        _expBar.Init(new StatBarData($"LVL {rootAbility.ShamanExperienceHandler.ShamanLevel}", rootAbility.ShamanExperienceHandler.CurrentExp, rootAbility.ShamanExperienceHandler.MaxExpToNextLevel));
        rootAbility.ShamanExperienceHandler.OnShamanGainExp += _expBar.UpdateStatbar;
        StatInit(rootAbility);
        base.Init(rootAbility);
    }

    private void StatInit(ShamanSaveData data)
    {
        _shamanStats = new UnitStats(data.Config.BaseStats);
        foreach (var statUpgrade in data.StatUpgrades)
        {
            foreach (var upgrade in statUpgrade.Stats)
            {
                _shamanStats.AddValueToStat(upgrade.StatType, upgrade.Factor, upgrade.StatValue);
            }
        }

        foreach (var statBlock in _statBlocks)
        {
            statBlock.Init(_shamanStats.Stats[statBlock.StatTypeId]);
        }
    }

    public void ShowStatBonus(StatType statType, float bonusValue)
    {
        foreach (var statBlock in _statBlocks)
        {
            if (statBlock.StatTypeId == statType)
            {
                statBlock.UpdateBonusStatUI(bonusValue);
                return;
            }
        }
    }

    public void AddUpgradeToStats(StatMetaUpgradeConfig statMetaUpgradeConfig)
    {
        statMetaUpgradeConfig.Stats.ForEach(stat => _shamanStats.AddValueToStat(stat.StatType, stat.Factor, stat.StatValue));
    }

    public override void Refresh()
    {
        if(!IsInitialized) return;
        _skillPointsText.text = _shaman.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        _levelText.text = "LVL " + _shaman.ShamanExperienceHandler.ShamanLevel;
    }

    public override void Hide()
    {
        if(_shaman != null) _shaman.ShamanExperienceHandler.OnShamanGainExp -= _expBar.UpdateStatbar;
        _expBar.Hide();
        base.Hide();
    }
}