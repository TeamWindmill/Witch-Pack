using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    [Header("Level")] public int ID;
    public ShamanConfig[] Shamans;
    public LevelHandler levelPrefab;
    

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
