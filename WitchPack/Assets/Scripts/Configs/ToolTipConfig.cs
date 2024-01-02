using UnityEngine;


[CreateAssetMenu(fileName = "ToolTipConfig", menuName = "Configs/UIConfigs/ToolTipConfig", order = 0)]
public class ToolTipConfig : ScriptableObject
{
    [SerializeField] private string[] _toolTips;

    public string GetToolTip()
    {
        return _toolTips[Random.Range(0, _toolTips.Length)];
    }
}