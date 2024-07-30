using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    public class BaseUnitConfig : BaseConfig
    {
        [SerializeField][PreviewField(75)]
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Right")]
        private Sprite unitSprite;
    
        [SerializeField][PreviewField(40)]
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Right")]
        private Sprite unitIcon;

        [SerializeField][PreviewField(40)]
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Right")]
        private Sprite unitIndicatorIcon;
    
        [BoxGroup("Unit")][TextArea][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Left")]
        [SerializeField] private string _description;
    
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Left")]
        [SerializeField] private OffensiveAbilitySO _autoAttack;
    
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Left")]
        [SerializeField] private Stats baseStats;
    
    


        public Sprite UnitSprite => unitSprite;
        public Sprite UnitIcon => unitIcon;
        public Stats BaseStats { get => baseStats; }
        public Sprite UnitIndicatorIcon { get => unitIndicatorIcon; }
        public OffensiveAbilitySO AutoAttack => _autoAttack;
        public string Description => _description;
    }
}
