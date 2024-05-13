using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackIcon : ClickableUIElement
{
    public event Action<ShamanConfig> OnIconClick;

    public ShamanConfig ShamanConfig { get; private set; }
    public bool Assigned { get; private set; }
    [SerializeField] private Image _splashRenderer;
    [SerializeField] private Image _bgRenderer;
    [SerializeField] private Color _flashColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private float _flashDuration;
    //private Sequence _sequence;

    public void Init()
    {
        // _sequence = DOTween.Sequence();
        // 
        // _sequence.Append();
        // _sequence.Append(_bgRenderer.DOColor(defaultColor, _flashDuration));
        UnassignShaman();
    }
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

    public void FlashInRed()
    {
        _bgRenderer.DOColor(_flashColor, _flashDuration).onComplete += () => _bgRenderer.DOColor(_defaultColor, _flashDuration);
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