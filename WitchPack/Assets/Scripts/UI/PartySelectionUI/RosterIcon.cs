using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class RosterIcon : ClickableUIElement
{
    public bool Available { get; private set; }
    public event Action<ShamanConfig,bool> OnIconClick;
    public ShamanConfig ShamanConfig { get; private set; }
    [SerializeField] private Image _spriteRenderer;
    [SerializeField] private Image _alphaCover;

    public Image SpriteRenderer => _spriteRenderer;

    public void Init(ShamanConfig config)
    {
        ShamanConfig = config;
        _spriteRenderer.sprite = config.UnitIcon;
        ToggleAvailable(true);
    }
    

    protected override void OnClick(PointerEventData eventData)
    {
        OnIconClick?.Invoke(ShamanConfig,Available);
    }

    public void ToggleAvailable(bool state)
    {
        _alphaCover.gameObject.SetActive(!state);
        Available = state;
    }
}