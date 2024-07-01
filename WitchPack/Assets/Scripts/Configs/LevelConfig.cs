using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    [BoxGroup("Level")] public int Number;
    [BoxGroup("Level")] public string Name;
    [BoxGroup("Level")] public LevelHandler levelPrefab;
    [BoxGroup("Level")] public ShamanConfig[] shamansToAddAfterComplete;
    [BoxGroup("Level")] public bool ShowTutorial;
    [BoxGroup("Level")] public ExpCalculatorConfig  ExpCalculatorConfig;
    [BoxGroup("Dialog")] public DialogSequence BeforeDialog;
    [BoxGroup("Dialog")] public DialogSequence StartDialog;
    [BoxGroup("Dialog")] public DialogSequence EndDialog;
    [BoxGroup("Dialog")] public DialogSequence AfterDialog;
    

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
