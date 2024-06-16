using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RosterIcon : ClickableUIElement
{
    public bool Available { get; private set; }
    public event Action<ShamanSaveData,bool> OnIconClick;
    public ShamanSaveData ShamanSaveData { get; private set; }
    [SerializeField] private Image _spriteRenderer;
    [SerializeField] private Image _alphaCover;

    public Image SpriteRenderer => _spriteRenderer;

    public void Init(ShamanSaveData saveData)
    {
        ShamanSaveData = saveData;
        _spriteRenderer.sprite = saveData.Config.UnitIcon;
        ToggleAvailable(true);
    }
    

    protected override void OnClick(PointerEventData eventData)
    {
        OnIconClick?.Invoke(ShamanSaveData,Available);
    }

    public void ToggleAvailable(bool state)
    {
        _alphaCover.gameObject.SetActive(!state);
        Available = state;
    }
}