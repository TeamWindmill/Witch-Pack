using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour
{
    public NodeState State;
    public bool IsActive => gameObject.activeSelf;
    
    [BoxGroup("Node")][SerializeField] protected ClickHelper _clickHelper;
    [BoxGroup("Node")][SerializeField] protected MapNode _childNode;
    [BoxGroup("Node")][SerializeField] private Transform _pathMask;
    [BoxGroup("Node")][SerializeField] private float _pathMaskSize;
    [BoxGroup("Node")][SerializeField] private float _pathMaskAnimationTime;
    [BoxGroup("Icon")][SerializeField] protected SpriteRenderer _spriteRenderer;
    [BoxGroup("Icon")][SerializeField] private Sprite _defaultIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _hoverIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _pressedIcon;
    

    protected virtual void Start()
    {
        _spriteRenderer.sprite = _defaultIcon;
        _clickHelper.OnClick += OnNodeClick;
        _clickHelper.OnEnterHover += OnNodeHoverEnter;
        _clickHelper.OnExitHover += OnNodeHoverExit;
        _clickHelper.OnMouseDown += OnNodeMouseDown;
        _clickHelper.OnMouseUp += OnNodeMouseUp;
    }
    public virtual void Init()
    {
        switch (State)
        {
            case NodeState.Completed:
                Complete();
                break;
            case NodeState.Open:
                Unlock();
                break;
            case NodeState.Locked:
                Lock();
                break;
        }
    }
    public virtual void SetState(NodeState nodeState)
    {
        State = nodeState;
    }
    public virtual void Lock()
    {
        gameObject.SetActive(false);
        State = NodeState.Locked;
    }
   
    public virtual void Unlock()
    {
        gameObject.SetActive(true); 
        State = NodeState.Open;
    }

    protected virtual void Complete()
    {
        State = NodeState.Completed;
        _childNode.State = NodeState.Open;
        gameObject.SetActive(true); 
        if (_childNode.State == NodeState.Completed)
            _pathMask.localScale = new Vector3(_pathMaskSize, _pathMaskSize, _pathMaskSize);
    }

    private void UnlockChild()
    {
        _childNode.Unlock();
    }

    protected virtual void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        
    }
    
    protected virtual void OnNodeHoverExit()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _defaultIcon;
        _pathMask.localScale = new Vector3(0, 0, 0);
    }

    protected virtual void OnNodeHoverEnter()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _hoverIcon;
        _pathMask.localScale = new Vector3(0, 0, 0);
        if(_childNode.State == NodeState.Open)
            _pathMask.DOScale(new Vector3(_pathMaskSize, _pathMaskSize, _pathMaskSize), _pathMaskAnimationTime).onComplete += UnlockChild;
    }
    
    protected virtual void OnNodeMouseDown(PointerEventData.InputButton obj)
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _pressedIcon;
    }

    protected virtual void OnNodeMouseUp(PointerEventData.InputButton obj)
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _defaultIcon;
    }
    protected virtual void OnDestroy()
    {
        _clickHelper.OnClick -= OnNodeClick;
    }
}

public enum NodeState
{
    Completed,
    Open,
    Locked
}