using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public List<MapNode> NodeObjects => _nodeObjects;
    public PartyTokenHandler PartyTokenHandler => _partyTokenHandler;

    [SerializeField] private List<MapNode> _nodeObjects;
    [SerializeField] private PartyTokenHandler _partyTokenHandler;

    [BoxGroup("Camera")] [SerializeField] private CameraLevelSettings _cameraLevelSettings;

    [BoxGroup("Temp")] [SerializeField] private ShamanConfig[] _shamanConfigsForInstantUnlock;

    public bool LevelSelectOpen;
    private const int CHALLENGES_AMOUNT = 3;


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
            levelSaves = new LevelSaveData[_nodeObjects.Count];
            for (int i = 0; i < _nodeObjects.Count; i++)
            {
                levelSaves[i] = new LevelSaveData(NodeState.Locked,CHALLENGES_AMOUNT);
                _nodeObjects[i].Init(i,levelSaves[i]);
            }

            GameManager.SaveData.LevelSaves = levelSaves;
        }
        else
        {
            for (int i = 0; i < _nodeObjects.Count; i++)
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

    private void OnValidate()
    {
        var nodes = FindObjectsOfType<MapNode>();
        foreach (var node in nodes)
        {
            if(!_nodeObjects.Contains(node)) _nodeObjects.Add(node);
        }
    }
}