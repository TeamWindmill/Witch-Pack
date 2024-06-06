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
        foreach (var statBlock in _statBlocks)
        {
            var stat = shaman.Stats[statBlock.StatTypeId];
            statBlock.Init(stat, _statBonusAdditionColor, _statBonusReductionColor);
            
        }

        foreach (var statBar in _statBarHandlers)
        {
            statBar.Init(shaman);
        }
    }
    

    public void UpdateStatBlocks(StatType shamanStatType, float newValue)
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

    public void HideStatBlocks()
    {
        foreach (var statBarHandler in _statBarHandlers)
        {
            statBarHandler.Hide();
        }
    }
}