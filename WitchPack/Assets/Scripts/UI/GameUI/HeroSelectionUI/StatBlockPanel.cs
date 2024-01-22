using UnityEngine;


public class StatBlockPanel : MonoBehaviour
{
    [SerializeField] private StatBlockUI[] _statBlocks;
    [SerializeField] private StatBarHandler[] _statBarHandlers;
    [SerializeField] private Color _statBonusAdditionColor;
    [SerializeField] private Color _statBonusReductionColor;

    public void Init(Shaman shaman)
    {
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

    public void UpdateStatBlocks(StatType shamanStatType, int newValue)
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
        foreach (var statBarHandler in _statBarHandlers)
        {
            statBarHandler.Hide();
        }
    }
}