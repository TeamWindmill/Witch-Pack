using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackIcon : ClickableUIElement
{
    public event Action<ShamanSaveData> OnIconLeftClick;
    public event Action<ShamanSaveData> OnIconRightClick;

    public ShamanSaveData ShamanSaveData { get; private set; }
    public bool Locked { get; private set; }
    public bool Assigned { get; private set; }
    [SerializeField] private Image _splashRenderer;
    [SerializeField] private Image _bgRenderer;
    [SerializeField] private Color _flashColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private float _flashDuration;


    public void Init()
    {
        UnassignShaman();
    }

    public void UnassignShaman()
    {
        ShamanSaveData = null;
        SetSplashAlpha(0);
        Assigned = false;
    }


    public void AssignShaman(ShamanSaveData shamanSaveData)
    {
        ShamanSaveData = shamanSaveData;
        _splashRenderer.sprite = shamanSaveData.Config.UnitIcon;
        SetSplashAlpha(1);
        Assigned = true;
    }

    public void FlashInRed()
    {
        _bgRenderer.DOColor(_flashColor, _flashDuration).onComplete += () => _bgRenderer.DOColor(_defaultColor, _flashDuration);
    }

    public void ToggleLockIcon(bool state)
    {
        Locked = state;
        if (state)
        {
            SetSplashAlpha(1);
            _splashRenderer.sprite = _lockIcon;
        }
        else
        {
            SetSplashAlpha(0);
        }
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (Assigned)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnIconLeftClick?.Invoke(ShamanSaveData);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnIconRightClick?.Invoke(ShamanSaveData);
            }
        }

        base.OnClick(eventData);
    }

    private void SetSplashAlpha(float value)
    {
        var color = _splashRenderer.color;
        color.a = value;
        _splashRenderer.color = color;
    }
}