using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIElement : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    //inherit from this class if it is a ui element
    public RectTransform RectTransform => rectTransform;
    
    [SerializeField, HideInInspector] protected RectTransform rectTransform;
    [SerializeField] private bool showOnAwake = false;
    [SerializeField] private bool assignUIGroup = false;
    [SerializeField,ShowIf(nameof(assignUIGroup))] protected UIGroup uiGroup;

    protected bool isMouseOver;

    protected virtual void Awake()
    {
        if (assignUIGroup) UIManager.Instance.AddUIElement(this,uiGroup);
        if (showOnAwake)
            Show();
        else
            gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void UpdateVisual()
    {
        
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnValidate()
    {
        rectTransform ??= GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        if (assignUIGroup)
        {
            if (UIManager.Instance is not null)
                UIManager.Instance.RemoveUIElement(this,uiGroup);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
