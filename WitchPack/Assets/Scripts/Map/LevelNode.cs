using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelNode : MonoBehaviour
{
    [BoxGroup("Level")][SerializeField] private LevelConfig _levelConfig;
    //[BoxGroup("Level")][SerializeField] private LevelNode[] _nextNodes;
    [BoxGroup("Level")][SerializeField] private LevelNode _parentNode;
    [BoxGroup("Icon")][SerializeField] private ClickHelper _clickHelper;
    [BoxGroup("Icon")][SerializeField] private SpriteRenderer _spriteRenderer;
    [BoxGroup("Icon")][SerializeField] private Color _winNodeColor;
    [BoxGroup("Icon")][SerializeField] private Color _avilableNodeColor;
    [BoxGroup("Icon")][SerializeField] private Sprite _defaultIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _hoverIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _pressedIcon;
    public bool IsLocked { get; private set; }
    public bool IsCompleted { get; private set; }

    //public int Id => _levelConfig.LevelId;

    private void Start()
    {
        _spriteRenderer.sprite = _defaultIcon;
        _clickHelper.OnClick += OnNodeClick;
        _clickHelper.OnEnterHover += OnNodeHoverEnter;
        _clickHelper.OnExitHover += OnNodeHoverExit;
        _clickHelper.OnMouseDown += OnNodeMouseDown;
        _clickHelper.OnMouseUp += OnNodeMouseUp;
    }

    public void Init(bool isCompleted, bool isUnLock)
    {
        IsLocked = isUnLock;
        if (IsLocked) Unlock();
        else Lock();
        IsCompleted = isCompleted;
        
        if(_parentNode is null) Unlock();
        else if(_parentNode.IsCompleted) Unlock();
    }

    public void Lock()
    {
        gameObject.SetActive(false);
        IsLocked = true;
    }

    private void OnDestroy()
    {
        _clickHelper.OnClick -= OnNodeClick;
    }

    public void Unlock()
    {
        gameObject.SetActive(true); 
        IsLocked = false;
    }

    private void Completed()
    {
        _spriteRenderer.color = _winNodeColor;
        
        // foreach (var node in _nextNodes)
        //     node.Unlock();
    }
    
    private void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.SetLevelConfig(_levelConfig);
        UIManager.Instance.ShowUIGroup(UIGroup.PartySelectionWindow);
    }
    
    private void OnNodeHoverExit()
    {
        _spriteRenderer.sprite = _defaultIcon;
    }

    private void OnNodeHoverEnter()
    {
        _spriteRenderer.sprite = _hoverIcon;
    }
    
    private void OnNodeMouseDown(PointerEventData.InputButton obj)
    {
        _spriteRenderer.sprite = _pressedIcon;
    }

    private void OnNodeMouseUp(PointerEventData.InputButton obj)
    {
        _spriteRenderer.sprite = _defaultIcon;
    }
}