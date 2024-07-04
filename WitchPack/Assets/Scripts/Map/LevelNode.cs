using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelNode : MapNode
{
    
    [BoxGroup("Level")][SerializeField] private LevelConfig _levelConfig;
    [BoxGroup("Icon")][SerializeField] private Color _winNodeColor;
    [BoxGroup("Icon")][SerializeField] private Color _avilableNodeColor;

    public override void Complete()
    {
        base.Complete();
        _spriteRenderer.color = _winNodeColor;
    }


    protected override void OnNodeClick(PointerEventData.InputButton button)
    {
        base.OnNodeClick(button);
        GameManager.Instance.SetLevelConfig(_levelConfig);
        UIManager.ShowUIGroup(UIGroup.LevelSelection);
    }
}

public class LevelSaveData
{
    public Dictionary<LevelChallengeType,bool>  ChallengesFirstTimes = new();
    public NodeState State;

    public LevelSaveData(NodeState state)
    {
        State = state;
        for (int i = 0; i < Enum.GetValues(typeof(LevelChallengeType)).Length; i++)
        {
            ChallengesFirstTimes.Add((LevelChallengeType)i,true);
        }
    }
}