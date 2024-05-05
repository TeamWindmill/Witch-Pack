using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackPanel : UIElement
{
    [SerializeField] private PackIcon[] _packIcons;

    private PartySelectionWindow _parent;

    public void Init(PartySelectionWindow parent)
    {
        _parent = parent;
        foreach (var icon in _packIcons)
        {
            icon.UnassignShaman();
            icon.OnIconClick += _parent.UnassignShamanFromPack;
        }
    }
    public void AddShamanToPack(ShamanConfig shaman)
    {
        foreach (var icon in _packIcons)
        {
            if(icon.Assigned) continue;
            icon.AssignShaman(shaman);
            return;
        }
    }
    public void RemoveShamanFromPack(ShamanConfig shaman)
    {
        foreach (var icon in _packIcons)
        {
            if (icon.ShamanConfig == shaman)
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
}
