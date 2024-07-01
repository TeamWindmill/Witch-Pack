using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelNode : MapNode
{
    [BoxGroup("Level")][SerializeField] private LevelConfig _levelConfig;
    [BoxGroup("Icon")][SerializeField] private Color _winNodeColor;
    [BoxGroup("Icon")][SerializeField] private Color _avilableNodeColor;

    protected override void Complete()
    {
        base.Complete();
        _spriteRenderer.color = _winNodeColor;
    }


    protected override void OnNodeClick(PointerEventData.InputButton button)
    {
        base.OnNodeClick(button);
        GameManager.Instance.SetLevelConfig(_levelConfig);
        UIManager.ShowUIGroup(UIGroup.PartySelectionWindow);
    }

    
}