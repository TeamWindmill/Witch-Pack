using Sirenix.Utilities;
using UnityEngine;


public class StatBlockPanel : UIElement
{
    [SerializeField] private StatBlockUI[] _statBlocks;
    [SerializeField] private StatBar[] _statBarHandlers;
    [SerializeField] private Color _statBonusAdditionColor;
    [SerializeField] private Color _statBonusReductionColor;

    private Shaman _shaman;

    public void Init(Shaman shaman)
    {
        _shaman = shaman;
        foreach (var statBlock in _statBlocks)
        {
            var stat = shaman.Stats[statBlock.StatTypeId];
            statBlock.Init(stat, _statBonusAdditionColor, _statBonusReductionColor);
            
        }

        foreach (var statBar in _statBarHandlers)
        {
            switch (statBar.StatBarType)
            {
                case StatBarType.Health:
                    statBar.Init(new StatBarData("Health", _shaman.Damageable.CurrentHp, _shaman.Damageable.MaxHp));
                    _shaman.Damageable.OnHealthChange += statBar.UpdateStatbar;
                    break;
                case StatBarType.Energy:
                    statBar.Init(new StatBarData("Energy", _shaman.EnergyHandler.CurrentEnergy, _shaman.EnergyHandler.MaxEnergyToNextLevel));
                    _shaman.EnergyHandler.OnShamanGainEnergy += statBar.UpdateStatbar;
                    break;
            }
        }
    }
    public void UpdateBonusStatBlocks(StatType shamanStatType, float newValue)
    {
        foreach (var statBlock in _statBlocks)
        {
            if (statBlock.StatTypeId == shamanStatType)
            {
                statBlock.UpdateBonusStatUI(newValue);
            }
        }
    }

    public void HideStatBlocksBonus()
    {
        foreach (var statBlock in _statBlocks)
        {
            statBlock.HideBonusStatUI();
        }
    }

    public override void Hide()
    {
        _statBlocks.ForEach(statBlock => statBlock.Hide());
        
        foreach (var statBar in _statBarHandlers)
        {
            switch (statBar.StatBarType)
            {
                case StatBarType.Health:
                    _shaman.Damageable.OnHealthChange -= statBar.UpdateStatbar;
                    break;
                case StatBarType.Energy:
                    _shaman.EnergyHandler.OnShamanGainEnergy -= statBar.UpdateStatbar;
                    break;
            }
        }
    }
}