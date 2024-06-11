using Sirenix.Utilities;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapNode[] _nodeObjects;

    [Header("Camera Control")] [SerializeField]
    private Vector2 _cameraLockedPos;

    [SerializeField] private int _cameraLockedZoom;

    [SerializeField] private bool[] _nodeLockState;
    [SerializeField] private ShamanConfig[] _shamanConfigsForInstantUnlock;
    [SerializeField] private LevelNode[] _testingLevelNodes;

    private void Awake()
    {
        Init(GameManager.SaveData.MapNodes);
    }

    private void Init(MapNode[] mapNodes)
    {
        if (mapNodes == null)
        {
            mapNodes = new MapNode[_nodeObjects.Length];
            for (int i = 0; i < _nodeObjects.Length; i++)
            {
                mapNodes[i] = _nodeObjects[i];
                _nodeObjects[i].Init();
                GameManager.SaveData.MapNodes = mapNodes;
            }
        }
        else
        {
            for (int i = 0; i < mapNodes.Length; i++)
            {
                //_nodeObjects[i] = mapNodes[i];
                _nodeObjects[i].SetState(mapNodes[i].State);
            }

            _nodeObjects.ForEach(node => node.Init());
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
                Init(GameManager.SaveData.MapNodes);
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