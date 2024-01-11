using System.Collections.Generic;
using UnityEngine;


public class PartyUIManager : UIElement
{
    [SerializeField] private RectTransform _heroContainer;
    [SerializeField] private ShamanUIHandler _shamanUIHanlder;

    public override void Show()
    {
        base.Show();
        var party = LevelManager.Instance.ShamanParty;
        foreach (var shaman in party)
        {
            var shamanUI = Instantiate(_shamanUIHanlder, _heroContainer);
            shamanUI.Init(shaman);
        }
    }
}