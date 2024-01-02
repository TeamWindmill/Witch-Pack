using UnityEngine;

public class NodeObject : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private BaseUnit[] _shamanConfigs;
    
    [SerializeField] private NodeObject[] _nextNodes;
    [SerializeField] private ClickHelper _clickHelper;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _winNodeColor;
    [SerializeField] private Color _avilableNodeColor;
    public bool IsUnlock { get; private set; }
    public bool IsCompleted { get; private set; }

    //public int Id => _levelConfig.LevelId;

    private void Start()
    {
        _clickHelper.OnClick += OnNodeClick;
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

    
    private void OnNodeClick()
    {
        GameManager.Instance.SetLevelConfig(_levelConfig);
       // set party data
        GameManager.SceneHandler.LoadScene(SceneType.Game);
    }
}