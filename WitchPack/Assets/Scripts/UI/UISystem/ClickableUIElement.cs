using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickableUIElement : UIElement, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    //inherit from this class for clickable ui element
    public event Action<PointerEventData> OnClickEvent;
    public event Action OnHoldClickEvent;
    public event Action OnDoubleClickEvent;

    [BoxGroup("UI Element/Clickable")] [SerializeField]
    private bool enableSoundOnClick = true;
    
    [BoxGroup("UI Element/Clickable")] [SerializeField]
    private bool enableDoubleClick;

    [BoxGroup("UI Element/Clickable")] [SerializeField, ShowIf(nameof(enableDoubleClick))]
    private float doubleClickSpeed = 0.5f;

    [BoxGroup("UI Element/Clickable")] [SerializeField]
    private bool enableHoldClick;

    [BoxGroup("UI Element/Clickable")] [SerializeField, ShowIf(nameof(enableHoldClick))]
    private float holdClickSpeed = 1.5f;

    private int _clickNum;

    protected float DoubleClickTimer;
    protected float HoldClickTimer;
    private bool _pointerDown;
    private bool _holdClickHappened;


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (_clickNum)
        {
            case 0:
                OnClick(eventData);
                return;
            case 1:
                OnDoubleClick();
                return;
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckDoubleClick();
        CheckHoldClick();
    }

    private void CheckHoldClick()
    {
        if (!enableHoldClick || _holdClickHappened) return;
        if (_pointerDown)
        {
            HoldClickTimer += Time.deltaTime;
            if (HoldClickTimer > holdClickSpeed)
            {
                OnHoldClick();
            }
        }
        else
        {
            HoldClickTimer = 0;
        }
    }

    private void CheckDoubleClick()
    {
        if (_clickNum == 0 || !enableDoubleClick)
            return;

        DoubleClickTimer -= Time.deltaTime;

        if (DoubleClickTimer <= 0)
        {
            _clickNum = 0;
            DoubleClickTimer = 0;
        }
    }

    protected virtual void OnClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(eventData);
        if(enableSoundOnClick) SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        if (!enableDoubleClick) return;

        _clickNum++;
        DoubleClickTimer = doubleClickSpeed;
    }

    protected virtual void OnDoubleClick()
    {
        OnDoubleClickEvent?.Invoke();
        DoubleClickTimer = 0;
        _clickNum = 0;
    }

    protected virtual void OnHoldClick()
    {
        OnHoldClickEvent?.Invoke();
        _holdClickHappened = true;
        HoldClickTimer = 0;
    }

    protected override void OnDisable()
    {
        _clickNum = 0;
        DoubleClickTimer = 0;
        base.OnDisable();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerDown = false;
        _holdClickHappened = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        _pointerDown = false;
        _holdClickHappened = false;
    }
}

public abstract class ClickableUIElement<T> : ClickableUIElement
{
    public bool Initialized { get; protected set; }

    public virtual void Init(T data)
    {
        Initialized = true;
    }
}