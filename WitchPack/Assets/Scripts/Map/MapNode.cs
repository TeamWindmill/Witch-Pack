using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour
{
    public NodeState State;
    
    [BoxGroup("Node")][SerializeField] protected ClickHelper _clickHelper;
    [BoxGroup("Node")][SerializeField] protected MapNode _parentNode;
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
    public virtual void Init(NodeState nodeState)
    {
        State = nodeState;
        switch (nodeState)
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
        
        if(_parentNode is null) Unlock();
        else if(_parentNode.State == NodeState.Completed) Unlock();
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
    }
    protected virtual void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        
    }
    
    protected virtual void OnNodeHoverExit()
    {
        _spriteRenderer.sprite = _defaultIcon;
    }

    protected virtual void OnNodeHoverEnter()
    {
        _spriteRenderer.sprite = _hoverIcon;
    }
    
    protected virtual void OnNodeMouseDown(PointerEventData.InputButton obj)
    {
        _spriteRenderer.sprite = _pressedIcon;
    }

    protected virtual void OnNodeMouseUp(PointerEventData.InputButton obj)
    {
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