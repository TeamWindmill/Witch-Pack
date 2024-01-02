using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    [Header("Level")]
    [SerializeField] private BaseUnit[] shamans;
    [SerializeField] private Transform levelMap;
    [Header("Camera")]
    [SerializeField] private int cameraStartZoom;
    [SerializeField] private Vector3 cameraStartPos;
}
