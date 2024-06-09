using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    [Header("Level")] public int Number;
    public string Name;
    public LevelHandler levelPrefab;
    public ShamanConfig[] shamansToAddAfterComplete;
    public bool ShowTutorial;
    

    [NonSerialized]public List<ShamanSaveData> SelectedShamans;
}

[Serializable]
public struct CameraLevelSettings
{
    public Vector2 CameraBorders;
    public int CameraMaxZoom;
    public bool OverrideCameraStartPos;
    [ShowIf(nameof(OverrideCameraStartPos))]public int CameraStartZoom;
    [ShowIf(nameof(OverrideCameraStartPos))]public Vector3 CameraStartPos;
}
