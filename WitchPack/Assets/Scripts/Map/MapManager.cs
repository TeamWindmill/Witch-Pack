using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Unity.Mathematics;
using UnityEngine;
using Path = DG.Tweening.Plugins.Core.PathCore.Path;

public class MapManager : MonoSingleton<MapManager>
{
    public MapNode[] NodeObjects => _nodeObjects;
    public PartyTokenHandler PartyTokenHandler => _partyTokenHandler;

    [SerializeField] private MapNode[] _nodeObjects;
    [SerializeField] private PartyTokenHandler _partyTokenHandler;
    
    [BoxGroup("Camera")][SerializeField] private CameraLevelSettings _cameraLevelSettings;
    
    [BoxGroup("Temp")][SerializeField] private ShamanConfig[] _shamanConfigsForInstantUnlock;
    [BoxGroup("Temp")][SerializeField] private LevelNode[] _testingLevelNodes;

    protected override void Awake()
    {
        base.Awake();
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

        _partyTokenHandler.ResetToken();
    }

    private void Start()
    {
        GameManager.Instance.CameraHandler.SetCameraLevelSettings(_cameraLevelSettings);
        GameManager.Instance.CameraHandler.ResetCamera();
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
    }

    public void ToggleTestingLevels(bool state)
    {
        foreach (var node in _testingLevelNodes)
        {
            node.gameObject.SetActive(state);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, _cameraLevelSettings.CameraBorders);
    }
}