using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectionWindow : UIElement
{
    public RosterPanel RosterPanel => _rosterPanel;
    public PackPanel PackPanel => _packPanel;

    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    [SerializeField] private Image _backgroundAlpha;

    public List<ShamanSaveData> ActiveShamanParty { get; private set; }
    public int MaxShamanPartyCap { get; private set; } = DEFAULT_PARTY_SIZE;

    private const int DEFAULT_PARTY_SIZE = 4;
    public int SelectedIconIndex { get; private set; }
    public bool SelectedMode { get; private set; }

    public override void Show()
    {
        ActiveShamanParty = new();
        _packPanel.Init(this);
        _rosterPanel.Init(this,GameManager.SaveData.ShamanRoster);
        _rosterPanel.Hide();
        _backgroundAlpha.gameObject.SetActive(false);
        AutoAssignShamansFromRoster();
        base.Show();
    }

    public override void Refresh()
    {
        _rosterPanel.Refresh();
    }

    public override void Hide()
    {
        if (SelectedMode)
        {
            ExitSelectMode();
        }
        // _rosterPanel.Hide();
        // _packPanel.Hide();
        // base.Hide();
    }

    public void EnterSelectMode(int index)
    {
        SelectedMode = true;
        SelectedIconIndex = index;
        _backgroundAlpha.gameObject.SetActive(true);
        _packPanel.SelectPackIcon(index);
        _rosterPanel.Show();
    }

    public void ExitSelectMode()
    {
        SelectedMode = false;
        _backgroundAlpha.gameObject.SetActive(false);
        _packPanel.ToggleIconsAlpha(false);
        _rosterPanel.Hide();
    }


    public void AssignShamanToSlot(ShamanSaveData shaman) => AssignShamanToSlot(shaman, SelectedIconIndex);
    public void AssignShamanToSlot(ShamanSaveData shaman, int slotIndex)
    {
        //if(!SelectedMode) return;
        if (_packPanel.PackIcons[slotIndex].Assigned)
        {
            RosterPanel.AddShamanBackToRoster(_packPanel.PackIcons[slotIndex].ShamanSaveData);
            _packPanel.RemoveShamanFromPack(shaman);
        }

        _packPanel.AddShamanToPack(shaman, slotIndex);
        RosterPanel.RemoveShamanFromRoster(shaman);
        ActiveShamanParty.Add(shaman);
    }

    public void UnassignShamanFromPack(ShamanSaveData shaman)
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

    public void AutoAssignShamansFromRoster()
    {
        foreach (var icon in _packPanel.PackIcons)
        {
            if (!icon.Assigned)
            {
                if (_rosterPanel.TryGetAvailableRosterIcon(out var rosterIcon))
                {
                    AssignShamanToSlot(rosterIcon.ShamanSaveData,icon.Index);
                }
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

    // protected override void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (SelectedMode && !UIManager.MouseOverUI)
    //         {
    //             ExitSelectMode();
    //         }
    //     }
    // }
}