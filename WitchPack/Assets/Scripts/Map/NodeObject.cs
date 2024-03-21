using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeObject : MonoBehaviour
{
    [BoxGroup("Level")][SerializeField] private LevelConfig _levelConfig;
    [BoxGroup("Level")][SerializeField] private NodeObject[] _nextNodes;
    [BoxGroup("Icon")][SerializeField] private ClickHelper _clickHelper;
    [BoxGroup("Icon")][SerializeField] private SpriteRenderer _spriteRenderer;
    [BoxGroup("Icon")][SerializeField] private Color _winNodeColor;
    [BoxGroup("Icon")][SerializeField] private Color _avilableNodeColor;
    [BoxGroup("Icon")][SerializeField] private Sprite _defaultIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _hoverIcon;
    [BoxGroup("Icon")][SerializeField] private Sprite _pressedIcon;
    public bool IsUnlock { get; private set; }
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
        IsUnlock = isUnLock;
        IsCompleted = isCompleted;
        
        if (IsUnlock)
            Unlock();
        if (isCompleted)
            Completed();
    }

    public void Lock()
    {
        gameObject.SetActive(false);
        IsUnlock = false;
    }

    private void OnDestroy()
    {
        _clickHelper.OnClick -= OnNodeClick;
    }

    public void Unlock()
    {
        _spriteRenderer.color = _avilableNodeColor;
        gameObject.SetActive(true); 
        //GameManager.GameData.SetLockNodeStat(_levelConfig.LevelId,true);
        IsUnlock = true;
    }

    private void Completed()
    {
        _spriteRenderer.color = _winNodeColor;
        
        foreach (var node in _nextNodes)
            node.Unlock();
    }
    
    private void OnNodeClick(PointerEventData.InputButton button)
    {
        if (button != PointerEventData.InputButton.Left) return;
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.SetLevelConfig(_levelConfig);
       // set party data
        GameManager.SceneHandler.LoadScene(SceneType.Game);
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