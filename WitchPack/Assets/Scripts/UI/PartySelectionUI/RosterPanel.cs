using System.Collections.Generic;
using UnityEngine;

public class RosterPanel : UIElement
{
    [SerializeField] private RosterIcon _rosterIconPrefab;
    [SerializeField] private Transform _holder;

    private List<RosterIcon> _rosterIcons = new();
    private PartySelectionWindow _parent;

    public void Init(PartySelectionWindow parent, List<ShamanSaveData> shamanSaveDatas)
    {
        Hide();
        _rosterIcons = new();
        _parent = parent;
        foreach (var shamanSaveData in shamanSaveDatas)
        {
            var icon = Instantiate(_rosterIconPrefab, _holder);
            _rosterIcons.Add(icon);
            icon.Init(shamanSaveData);
            icon.OnIconClick += ToggleShaman;
        }
        base.Show();
    }

    public override void Hide()
    {
        if (_rosterIcons.Count > 0)
        {
            foreach (var icon in _rosterIcons)
            {
                Destroy(icon.gameObject);
            }
            _rosterIcons.Clear();
        }
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
        if (available) _parent.AssignShamanToParty(saveData);
        else _parent.UnassignShamanFromParty(saveData);
    }
}
