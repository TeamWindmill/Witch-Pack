using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private LevelNode[] _nodeObjects;
    [SerializeField] private bool _unLockAll;

    [Header("Camera Control")] [SerializeField]
    private Vector2 _cameraLockedPos;

    [SerializeField] private int _cameraLockedZoom;

    [SerializeField] private bool[] _nodeLockState;
    [SerializeField] private ShamanConfig[] _shamanConfigsForInstantUnlock;
    [SerializeField] private LevelNode[] _testingLevelNodes;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _nodeObjects.Length; i++)
        {
            _nodeObjects[i].Init(GameManager.Instance.LevelsCompleted[i], _nodeLockState[i]);
        }
    }

    private void Start()
    {
        GameManager.Instance.CameraHandler.LockCamera(_cameraLockedPos, _cameraLockedZoom);
    }

    public void UnlockLevels(bool state)
    {
        foreach (var nodeObject in _nodeObjects)
        {
            if (state)
                nodeObject.Unlock();
            else
                Init();
        }
    }

    public void UnlockShamans(bool state)
    {
        GameManager.Instance.ShamansManager.AddShamanToRoster(_shamanConfigsForInstantUnlock);
        //GameManager.Instance.ShamansManager.re(_shamanConfigsForInstantUnlock);
    }

    public void ToggleTestingLevels(bool state)
    {
        foreach (var node in _testingLevelNodes)
        {
            node.gameObject.SetActive(state);
        }
    }
}