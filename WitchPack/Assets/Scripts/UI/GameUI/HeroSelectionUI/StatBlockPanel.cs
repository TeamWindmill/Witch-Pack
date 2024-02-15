using UnityEngine;


public class StatBlockPanel : MonoBehaviour
{
    [SerializeField] private StatBlockUI[] _statBlocks;
    [SerializeField] private StatBarHandler[] _statBarHandlers;
    [SerializeField] private Color _statBonusAdditionColor;
    [SerializeField] private Color _statBonusReductionColor;

    private Shaman _shaman;

    public void Init(Shaman shaman)
    {
        _shaman = shaman;
        _shaman.Stats.OnStatChanged += OnBaseStatChange;
        foreach (var statBlock in _statBlocks)
        {
            var statValue = shaman.Stats.GetStatValue(statBlock.StatTypeId);
            statBlock.Init(statValue, _statBonusAdditionColor, _statBonusReductionColor);
        }

        foreach (var statBar in _statBarHandlers)
        {
            statBar.Init(shaman);
        }
    }

    private void OnBaseStatChange(StatType statType, float delta)
    {
        foreach (var statBlock in _statBlocks)
        {
            if(statBlock.StatTypeId == statType) statBlock.UpdateBaseStat(delta);
        }
    }

    public void UpdateStatBlocks(StatType shamanStatType, float newValue)
    {
        foreach (var statBlock in _statBlocks)
        {
            if (statBlock.StatTypeId == shamanStatType)
            {
                statBlock.UpdateUI(newValue);
            }
        }
    }

    public void HideStatBlocks()
    {
        _shaman.Stats.OnStatChanged -= OnBaseStatChange;

        foreach (var statBarHandler in _statBarHandlers)
        {
            statBarHandler.Hide();
        }
    }
}