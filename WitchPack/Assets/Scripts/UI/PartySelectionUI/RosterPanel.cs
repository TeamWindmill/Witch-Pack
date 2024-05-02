using System.Collections.Generic;
using UnityEngine;

public class RosterPanel : UIElement
{
    [SerializeField] private RosterIcon _rosterIconPrefab;
    [SerializeField] private List<RosterIcon> _rosterIcons;
    [SerializeField] private Transform _holder;

    public void Init(List<ShamanConfig> configs)
    {
        foreach (var config in configs)
        {
            var icon = Instantiate(_rosterIconPrefab, _holder);
            _rosterIcons.Add(icon);
            icon.SpriteRenderer.sprite = config.UnitIcon;
        }
        base.Show();
    }
}
