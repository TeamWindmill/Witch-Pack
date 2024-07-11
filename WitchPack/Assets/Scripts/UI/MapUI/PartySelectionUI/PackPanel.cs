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

        for (int i = 0; i < _packIcons.Length; i++)
        {
            _packIcons[i].Init(i);
            _packIcons[i].OnIconLeftClick += OnIconClick;
        }

        base.Show();
    }

    public override void Hide()
    {
        foreach (var icon in _packIcons)
        {
            icon.OnIconLeftClick -= OnIconClick;
        }

        base.Hide();
    }

    public void SelectPackIcon(int index)
    {
        ToggleIconsAlpha(true);
        _packIcons[index].ToggleAlpha(false);
    }

    public void ToggleIconsAlpha(bool state)
    {
        foreach (var icon in _packIcons)
        {
            icon.ToggleAlpha(state);
        }
    }

    public void AddShamanToPack(ShamanSaveData shaman, int iconIndex)
    {
        _packIcons[iconIndex].AssignShaman(shaman);
    }

    public void RemoveShamanFromPack(ShamanSaveData shaman)
    {
        foreach (var icon in _packIcons)
        {
            if (icon.ShamanSaveData == shaman)
            {
                icon.UnassignShaman();
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

    // private void RefreshPackPanel()
    // {
    //     foreach (var icon in _packIcons)
    //     {
    //         if(icon.Locked) continue;
    //         icon.UnassignShaman();
    //     }
    //
    //     for (int i = 0; i < _parent.ActiveShamanParty.Count; i++)
    //     {
    //         _packIcons[i].AssignShaman(_parent.ActiveShamanParty[i]);
    //     }
    // }
    public void ReduceShamanSlots(int availableSlots)
    {
        for (int i = 0; i < _packIcons.Length; i++)
        {
            if (i >= availableSlots)
            {
                if (_packIcons[i].Assigned) _parent.UnassignShamanFromPack(_packIcons[i].ShamanSaveData);
                _packIcons[i].ToggleLockIcon(true);
            }
            else
            {
                if (!_packIcons[i].Assigned)
                    _packIcons[i].ToggleLockIcon(false);
            }
        }
    }

    private void OnIconClick(int index)
    {
        if (_packIcons[index].Assigned) _parent.UnassignShamanFromPack(_packIcons[index].ShamanSaveData);
        _parent.EnterSelectMode(index);
    }
}