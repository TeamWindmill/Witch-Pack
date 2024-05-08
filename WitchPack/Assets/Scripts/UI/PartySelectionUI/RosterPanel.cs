using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RosterPanel : UIElement
{
    [SerializeField] private RosterIcon _rosterIconPrefab;
    [SerializeField] private Transform _holder;

    private List<RosterIcon> _rosterIcons = new();
    private PartySelectionWindow _parent;

    public void Init(PartySelectionWindow parent, List<ShamanSaveData> configs)
    {
        _parent = parent;
        foreach (var shamanSaveData in configs)
        {
            var icon = Instantiate(_rosterIconPrefab, _holder);
            _rosterIcons.Add(icon);
            icon.Init(shamanSaveData.Config);
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

    public void AssignShaman(ShamanConfig shamanConfig) 
    {
        foreach (var icon in _rosterIcons)
        {
            if (icon.ShamanConfig == shamanConfig)
            {
                icon.ToggleAvailable(false);
                return;
            }
        }
    }
    public void UnassignShaman(ShamanConfig shamanConfig) 
    {
        foreach (var icon in _rosterIcons)
        {
            if (icon.ShamanConfig == shamanConfig)
            {
                icon.ToggleAvailable(true);
                return;
            }
        }
    }

    private void ToggleShaman(ShamanConfig config, bool available)
    {
        if (available) _parent.AssignShamanToPack(config);
        else _parent.UnassignShamanFromPack(config);
    }
}
