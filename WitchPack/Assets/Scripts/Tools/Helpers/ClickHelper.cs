using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHelper : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<PointerEventData.InputButton> OnClick;
    public event Action OnEnterHover;
    public event Action OnExitHover;

    public bool IsHover { get; private set; }

    private const int CLICKABLE_LAYER_INDEX = 11;

    private void Awake()
    {
        gameObject.layer = CLICKABLE_LAYER_INDEX;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(eventData.button);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHover = true;
        OnEnterHover?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHover = false;
        OnExitHover?.Invoke();
    }
}