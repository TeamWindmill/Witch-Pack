using System.Collections.Generic;
using Configs;
using Managers;
using Sirenix.OdinInspector;
using UI.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    public class LevelNode : MapNode
    {
    
        [BoxGroup("Level")][SerializeField] private LevelConfig _levelConfig;
        [BoxGroup("Icon")][SerializeField] private Color _winNodeColor;
        [BoxGroup("Icon")][SerializeField] private Color _avilableNodeColor;

        private void Awake()
        {
            _levelConfig.SetIndexes();
        }

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
        public List<bool> ChallengesFirstTimes = new();
        public NodeState State;

        public LevelSaveData(NodeState state,int challengesAmount)
        {
            State = state;
            for (int i = 0; i < challengesAmount; i++)
            {
                ChallengesFirstTimes.Add(true);
            }
        }
    }
}