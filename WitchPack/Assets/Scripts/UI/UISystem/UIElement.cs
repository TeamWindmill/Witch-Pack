using System;
using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    //inherit from this class if it is a ui element
    [SerializeField, HideInInspector] protected RectTransform rectTransform;
    [SerializeField] private bool _showOnAwake = true;
    [SerializeField] private UIGroup uiGroup;

    public RectTransform RectTransform => rectTransform;

    protected virtual void Start()
    {
        UIManager.Instance.AddUIElement(this,uiGroup);
        
        if (_showOnAwake)
            Show();
        else
            gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        
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
        if (UIManager.Instance is not null)
            UIManager.Instance.RemoveUIElement(this,uiGroup);
    }
}
