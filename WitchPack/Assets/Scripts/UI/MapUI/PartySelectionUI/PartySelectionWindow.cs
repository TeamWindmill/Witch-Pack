using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartySelectionWindow : UIElement
{
    public RosterPanel RosterPanel => _rosterPanel;
    public PackPanel PackPanel => _packPanel;

    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    
    public List<ShamanSaveData> ActiveShamanParty { get; private set; }
    public int MaxShamanPartyCap { get; private set; } = DEFAULT_PARTY_SIZE;

    private const int DEFAULT_PARTY_SIZE = 4;
    public override void Show()
    {
        ActiveShamanParty = new();
        _rosterPanel.Init(this, GameManager.ShamansManager.ShamanRoster);
        _packPanel.Init(this);
        
        AutoAssignShamansFromRoster();
        base.Show();
    }

    public override void Refresh()
    {
        _rosterPanel.Init(this, GameManager.ShamansManager.ShamanRoster);
        _packPanel.Init(this);
    }

    public override void Hide()
    {
        _rosterPanel.Hide();
        _packPanel.Hide();
        base.Hide();
    }
    
    public void AssignShamanToParty(ShamanSaveData shaman)
    {
        if(ActiveShamanParty.Count >= MaxShamanPartyCap) return;
        ActiveShamanParty.Add(shaman);
        _packPanel.AddShamanToPack(shaman);
        _rosterPanel.RemoveShamanFromRoster(shaman);
    }

    public void UnassignShamanFromParty(ShamanSaveData shaman)
    {
        ActiveShamanParty.Remove(shaman);
        _rosterPanel.AddShamanBackToRoster(shaman);
        _packPanel.RemoveShamanFromPack(shaman);
    }

    public void ReduceShamanSlots(int amountToReduce)
    {
        MaxShamanPartyCap = DEFAULT_PARTY_SIZE;
        MaxShamanPartyCap -= amountToReduce;
        _packPanel.ReduceShamanSlots(MaxShamanPartyCap);
    }

    private void AutoAssignShamansFromRoster()
    {
        var assignedShamans = 0;
        foreach (var packIcon in _packPanel.PackIcons)
        {
            if (packIcon.Assigned) assignedShamans++;
        }
        if(assignedShamans > 0) return;
        foreach (var icon in _rosterPanel.RosterIcons)
        {
            if (icon.ShamanSaveData != null)
            {
                AssignShamanToParty(icon.ShamanSaveData);
            }
        }
    }

    public void RefreshActiveParty()
    {
        ActiveShamanParty = new List<ShamanSaveData>();

        foreach (var icon in _packPanel.PackIcons)
        {
            if (icon.Assigned)
            {
                ActiveShamanParty.Add(icon.ShamanSaveData);
            }
        }
    }

    public void FlashInRed()
    {
        _packPanel.FlashInRed();
    }
}