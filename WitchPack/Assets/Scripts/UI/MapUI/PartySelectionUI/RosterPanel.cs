using System.Collections.Generic;
using UnityEngine;

public class RosterPanel : UIElement
{
    public List<RosterIcon> RosterIcons => _rosterIcons;

    [SerializeField] private RosterIcon _rosterIconPrefab;
    [SerializeField] private Transform _holder;

    private List<RosterIcon> _rosterIcons = new();
    private PartySelectionWindow _parent;

    public void Init(PartySelectionWindow parent, List<ShamanSaveData> shamanSaveDatas)
    {
        if (_rosterIcons.Count > 0)
        {
            foreach (var icon in _rosterIcons)
            {
                Destroy(icon.gameObject);
            }
            _rosterIcons.Clear();
        }
        
        _parent = parent;
        foreach (var shamanSaveData in shamanSaveDatas)
        {
            var icon = Instantiate(_rosterIconPrefab, _holder);
            _rosterIcons.Add(icon);
            icon.Init(shamanSaveData);
        }
    }

    public override void Show()
    {
        foreach (var icon in _rosterIcons)
        {
            icon.OnIconClick += ToggleShaman;
        }
        base.Show();
    }

    public override void Refresh()
    {
        foreach (var shamanSaveData in GameManager.SaveData.ShamanRoster)
        {
            var exists = false;
            foreach (var rosterIcon in _rosterIcons)
            {
                if (rosterIcon.ShamanSaveData == shamanSaveData) exists = true;
            }
            if(exists) continue;
            var icon = Instantiate(_rosterIconPrefab, _holder);
            _rosterIcons.Add(icon);
            icon.Init(shamanSaveData);
        }
    }

    public override void Hide()
    {
        foreach (var icon in _rosterIcons)
        {
            icon.OnIconClick -= ToggleShaman;
        }
        base.Hide();
    }

    public bool TryGetAvailableRosterIcon(out RosterIcon rosterIcon)
    {
        foreach (var icon in _rosterIcons)
        {
            if (icon.Available)
            {
                rosterIcon = icon;
                return true;
            }
        }
        rosterIcon = null;
        return false;
    }

    public void RemoveShamanFromRoster(ShamanSaveData shamanSaveData) 
    {
        foreach (var icon in _rosterIcons)
        {
            if (icon.ShamanSaveData == shamanSaveData)
            {
                icon.ToggleAvailable(false);
                return;
            }
        }
    }
    public void AddShamanBackToRoster(ShamanSaveData shamanSaveData) 
    {
        foreach (var icon in _rosterIcons)
        {
            if (icon.ShamanSaveData == shamanSaveData)
            {
                icon.ToggleAvailable(true);
                return;
            }
        }
    }

    private void ToggleShaman(ShamanSaveData saveData, bool available)
    {
        if (available) _parent.AssignShamanToSlot(saveData);
        else _parent.UnassignShamanFromPack(saveData);
    }
}
