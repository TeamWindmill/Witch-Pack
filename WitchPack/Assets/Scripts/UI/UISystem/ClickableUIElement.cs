using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickableUIElement : UIElement,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //inherit from this class for clickable ui element
    public event Action OnClickEvent;
    public event Action OnDoubleClickEvent;
    public event Action OnEnter;
    public event Action OnExit;
    
    [SerializeField] private bool enableDoubleClick;

    [SerializeField, ShowIf(nameof(enableDoubleClick))]
    private float doubleClickSpeed = 0.5f;
    
    private int _clickNum;

    private float _doubleClickTimer;
    
    public virtual void OnPointerEnter(PointerEventData eventData) => OnEnter?.Invoke();

    public virtual void OnPointerExit(PointerEventData eventData) => OnExit?.Invoke();
    

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (_clickNum)
        {
            case 0:
                OnClick(eventData);
                return;
            case 1:
                OnDoubleClick(eventData);
                return;
        }
    }

    private void Update()
    {
        if (_clickNum == 0 || !enableDoubleClick)
            return;

        _doubleClickTimer -= Time.deltaTime;

        if (_doubleClickTimer <= 0)
        {
            _clickNum = 0;
            _doubleClickTimer = 0;
        }
    }

    protected virtual void OnClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();

        if (!enableDoubleClick) return;

        _clickNum++;
        _doubleClickTimer = doubleClickSpeed;
    }
    protected virtual void OnDoubleClick(PointerEventData eventData)
    {
        OnDoubleClickEvent?.Invoke();
        _doubleClickTimer = 0;
        _clickNum = 0;
    }
    
    protected virtual void OnDisable()
    {
        _clickNum = 0;
        _doubleClickTimer = 0;
    }
}
