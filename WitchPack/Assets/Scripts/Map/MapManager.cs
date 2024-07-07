using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public MapNode[] NodeObjects => _nodeObjects;
    public PartyTokenHandler PartyTokenHandler => _partyTokenHandler;

    [SerializeField] private MapNode[] _nodeObjects;
    [SerializeField] private PartyTokenHandler _partyTokenHandler;

    [BoxGroup("Camera")] [SerializeField] private CameraLevelSettings _cameraLevelSettings;

    [BoxGroup("Temp")] [SerializeField] private ShamanConfig[] _shamanConfigsForInstantUnlock;
    [BoxGroup("Temp")] [SerializeField] private LevelNode[] _testingLevelNodes;

    public bool LevelSelectOpen;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        _nodeObjects.ForEach(node => node.OnMouseEnter += AnimateToken);
        _nodeObjects.ForEach(node => node.OnClick += (node) => LevelSelectOpen = true);

        GameManager.CameraHandler.SetCameraLevelSettings(_cameraLevelSettings);
        GameManager.CameraHandler.ResetCamera();
    }

    public void Init()
    {
        LevelSelectOpen = false;
        var levelSaves = GameManager.SaveData.LevelSaves;
        if (levelSaves == null)
        {
            levelSaves = new LevelSaveData[_nodeObjects.Length];
            for (int i = 0; i < _nodeObjects.Length; i++)
            {
                levelSaves[i] = new LevelSaveData(NodeState.Locked);
                _nodeObjects[i].Init(i,levelSaves[i]);
            }

            GameManager.SaveData.LevelSaves = levelSaves;
        }
        else
        {
            for (int i = 0; i < _nodeObjects.Length; i++)
            {
                _nodeObjects[i].Init(i,levelSaves[i]);
            }
            GameManager.SaveData.CurrentNode = _nodeObjects[GameManager.SaveData.LastLevelCompletedIndex];
        }

        _partyTokenHandler.ResetToken();
    }

    public void ResetVisuals()
    {
        _partyTokenHandler.ClearPath();
        foreach (MapNode mapNode in _nodeObjects)
        {
            switch (mapNode.LevelSaveData.State)
            {
                case NodeState.Completed:
                    mapNode.Path?.TogglePathMask(SpriteMaskInteraction.None);
                    break;
                case NodeState.Open:
                    if(mapNode == GameManager.SaveData.CurrentNode)
                        mapNode.Path?.TogglePathMask(SpriteMaskInteraction.None);
                    else
                        mapNode.Path?.TogglePathMask(SpriteMaskInteraction.VisibleInsideMask);
                    break;
                case NodeState.Locked:
                    break;
            }
        }
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
        GameManager.ShamansManager.AddShamanToRoster(_shamanConfigsForInstantUnlock);
        UIManager.RefreshUIGroup(UIGroup.PartySelectionWindow);
    }

    public void ToggleTestingLevels(bool state)
    {
        foreach (var node in _testingLevelNodes)
        {
            node.gameObject.SetActive(state);
        }
    }

    private void AnimateToken(MapNode mapNode)
    {
        if(LevelSelectOpen) return;
        if(mapNode.LevelSaveData.State != NodeState.Open) return;
        if (mapNode.Path != null)
        {
            var startPos = _nodeObjects[mapNode.Index - 1].transform.position;
            PartyTokenHandler.AnimateToken(startPos, mapNode.Path.GetPathPoints(startPos), mapNode.Path.PathDuration);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, _cameraLevelSettings.CameraBorders);
    }
}