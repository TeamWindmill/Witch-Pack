using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour
{
    public NodeState State { get; private set; }
    public bool IsActive => gameObject.activeSelf;
    
    [BoxGroup("Node")][SerializeField] protected ClickHelper _clickHelper;
    [BoxGroup("Node")][SerializeField] protected MapNode _parentNode;
    [BoxGroup("Node")][SerializeField] private Transform _path;
    //[BoxGroup("Node")][SerializeField] private float _pathMaskSize;
    //[BoxGroup("Node")][SerializeField] private float _pathMaskAnimationTime;
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
        
        if(_parentNode == null || _parentNode.State == NodeState.Completed) Unlock();
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
        gameObject.SetActive(true); 
    }

    protected virtual void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.SaveData.CurrentNode = this; //temp
        GameManager.Instance.CameraHandler.SetCameraPosition(transform.position);
    }
    
    protected virtual void OnNodeHoverExit()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _defaultIcon;
        MapManager.Instance.PartyTokenHandler.ResetToken();
    }

    protected virtual void OnNodeHoverEnter()
    {
        if (!IsActive) return;
        _spriteRenderer.sprite = _hoverIcon;
        
        if(_path != null) MapManager.Instance.PartyTokenHandler.AnimateToken(_path,2); 
       
        
        
        // _pathMask.localScale = new Vector3(0, 0, 0);
        // if(_childNode.State == NodeState.Open)
        //     _pathMask.DOScale(new Vector3(_pathMaskSize, _pathMaskSize, _pathMaskSize), _pathMaskAnimationTime).onComplete += UnlockChild;
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