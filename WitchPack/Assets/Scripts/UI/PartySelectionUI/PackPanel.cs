using Unity.VisualScripting;
using UnityEngine;

public class PackPanel : UIElement
{
    [SerializeField] private PackIcon[] _packIcons;

    private PartySelectionWindow _parent;

    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel; //tempi temp

    public void Init(PartySelectionWindow parent)
    {
        _parent = parent;
        foreach (var icon in _packIcons)
        {
            icon.Init();
            icon.OnIconLeftClick += _parent.UnassignShamanFromPack;
            icon.OnIconRightClick += OpenUpgradePanel;
        }
    }
    public void AddShamanToPack(ShamanSaveData shaman)
    {
        foreach (var icon in _packIcons)
        {
            if(icon.Assigned) continue;
            icon.AssignShaman(shaman);
            return;
        }
    }
    public void RemoveShamanFromPack(ShamanSaveData shaman)
    {
        foreach (var icon in _packIcons)
        {
            if (icon.ShamanSaveData == shaman)
            {
                icon.UnassignShaman();
                RefreshPackPanel();
                return;
            }
        }
    }

    private void RefreshPackPanel()
    {
        foreach (var icon in _packIcons)
        {
            icon.UnassignShaman();
        }

        for (int i = 0; i < _parent.ActiveShamanParty.Count; i++)
        {
            _packIcons[i].AssignShaman(_parent.ActiveShamanParty[i]);
        }
    }
    public void FlashInRed()
    {
        foreach (var icon in _packIcons)
        {
            icon.FlashInRed();
        }
    }
    private void OpenUpgradePanel(ShamanSaveData shamanSaveData)
    {
        _shamanUpgradePanel.Init(shamanSaveData.Config.ShamanMetaUpgradeConfig);
    }
    
}
