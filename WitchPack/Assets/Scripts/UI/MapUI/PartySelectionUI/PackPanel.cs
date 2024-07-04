using UnityEngine;

public class PackPanel : UIElement
{
    public PackIcon[] PackIcons => _packIcons;

    [SerializeField] private PackIcon[] _packIcons;

    private PartySelectionWindow _parent;

    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;

    public void Init(PartySelectionWindow parent)
    {
        _parent = parent;
        foreach (var icon in _packIcons)
        {
            icon.Init();
            icon.OnIconLeftClick += _parent.UnassignShamanFromParty;
            icon.OnIconRightClick += OpenUpgradePanel;
        }
        base.Show();
    }

    public override void Hide()
    {
        foreach (var icon in _packIcons)
        {
            icon.OnIconLeftClick -= _parent.UnassignShamanFromParty;
            icon.OnIconRightClick -= OpenUpgradePanel;
        }
        base.Hide();
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
    public void FlashInRed()
    {
        foreach (var icon in _packIcons)
        {
            icon.FlashInRed();
        }
    }
    private void RefreshPackPanel()
    {
        foreach (var icon in _packIcons)
        {
            if(icon.Locked) continue;
            icon.UnassignShaman();
        }

        for (int i = 0; i < _parent.ActiveShamanParty.Count; i++)
        {
            _packIcons[i].AssignShaman(_parent.ActiveShamanParty[i]);
        }
    }
    private void OpenUpgradePanel(ShamanSaveData shamanSaveData)
    {
        _shamanUpgradePanel.Init(shamanSaveData);
    }
    public void ReduceShamanSlots(int availableSlots)
    {
        for (int i = 0; i < _packIcons.Length; i++)
        {
            if (i >= availableSlots)
            {
                if(_packIcons[i].ShamanSaveData != null) _parent.UnassignShamanFromParty(_packIcons[i].ShamanSaveData);
                _packIcons[i].ToggleLockIcon(true);
            }
            else
            {
                if(_packIcons[i].ShamanSaveData == null)
                    _packIcons[i].ToggleLockIcon(false);
            }
        }
    }
}
