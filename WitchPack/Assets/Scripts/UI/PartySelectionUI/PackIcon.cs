using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackIcon : ClickableUIElement
{
    public event Action<ShamanConfig> OnIconClick;

    public ShamanConfig ShamanConfig { get; private set; }
    public bool Assigned { get; private set; }
    [SerializeField] private Image _splashRenderer;


    public void UnassignShaman()
    {
        ShamanConfig = null;
        var color = _splashRenderer.color;
        color.a = 0;
        _splashRenderer.color = color;
        Assigned = false;
    }

    public void AssignShaman(ShamanConfig shamanConfig)
    {
        ShamanConfig = shamanConfig;
        _splashRenderer.sprite = shamanConfig.UnitIcon;
        var color = _splashRenderer.color;
        color.a = 1;
        _splashRenderer.color = color;
        Assigned = true;
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (Assigned)
        {
            OnIconClick?.Invoke(ShamanConfig); //might change later to show information
        }
        
        base.OnClick(eventData);
    }
}