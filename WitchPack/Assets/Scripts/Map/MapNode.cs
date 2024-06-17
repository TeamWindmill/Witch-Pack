using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class MapNode : MonoBehaviour
{
    public event Action<MapNode> OnMouseEnter;
    public event Action<MapNode> OnMouseExit;
    public event Action<MapNode> OnClick;
    
    public NodeState State { get; private set; } = NodeState.Locked;
    public int Index { get; private set; }
    public bool IsActive => gameObject.activeSelf;
    public LevelPath Path => _path;

    [BoxGroup("Node")][SerializeField] protected ClickHelper _clickHelper;
    [BoxGroup("Node")][SerializeField] protected MapNode _parentNode;
    [BoxGroup("Node")][SerializeField] private LevelPath _path;
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
    public virtual void Init(int index)
    {
        Index = index;
        switch (State)
        {
            case NodeState.Completed:
                Complete();
                break;
            case NodeState.Open:
                Unlock();
                break;
            case NodeState.Locked:
                if(_parentNode == null || _parentNode.State == NodeState.Completed) Unlock();
                else Lock();
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
        _path?.TogglePathMask(SpriteMaskInteraction.VisibleInsideMask);
        State = NodeState.Open;
    }


    protected virtual void Complete()
    {
        State = NodeState.Completed;
        _path?.TogglePathMask(SpriteMaskInteraction.None);
        gameObject.SetActive(true); 
    }

    protected virtual void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.SaveData.CurrentNode = this; //temp
        GameManager.Instance.CameraHandler.SetCameraPosition(transform.position);
        _path?.TogglePathMask(SpriteMaskInteraction.None);
        MapManager.Instance.ResetVisuals();
        OnClick?.Invoke(this);
    }
    
    protected virtual void OnNodeHoverExit()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _defaultIcon;
        MapManager.Instance.PartyTokenHandler.ResetToken();
        OnMouseExit?.Invoke(this);
    }

    protected virtual void OnNodeHoverEnter()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _hoverIcon;
        OnMouseEnter?.Invoke(this);
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