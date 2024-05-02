using UnityEngine;

public class PartySelectionWindow : UIElement
{
    [SerializeField] private RosterPanel _rosterPanel;

    public override void Show()
    {
        _rosterPanel.Init(GameManager.Instance.ShamanRoster);
        base.Show();
    }
}
