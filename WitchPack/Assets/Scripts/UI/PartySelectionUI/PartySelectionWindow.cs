using System.Collections.Generic;
using UnityEngine;

public class PartySelectionWindow : UIElement
{
    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    public List<ShamanConfig> ActiveShamanParty { get;} = new();

    public override void Show()
    {
        _rosterPanel.Init(this,GameManager.Instance.ShamanRoster);
        _packPanel.Init(this);
        base.Show();
    }

    public void StartLevel()
    {
        GameManager.Instance.CurrentLevelConfig.Shamans = ActiveShamanParty.ToArray();
        GameManager.SceneHandler.LoadScene(SceneType.Game);
    }

    public void AssignShamanToPack(ShamanConfig shaman)
    {
        ActiveShamanParty.Add(shaman);
        _packPanel.AddShamanToPack(shaman);
        _rosterPanel.AssignShaman(shaman);
    }
    public void UnassignShamanFromPack(ShamanConfig shaman)
    {
        ActiveShamanParty.Remove(shaman);
        _rosterPanel.UnassignShaman(shaman);
        _packPanel.RemoveShamanFromPack(shaman);
    }
}
