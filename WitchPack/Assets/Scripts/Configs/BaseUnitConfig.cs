using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnitConfig : BaseConfig
{
    [SerializeField][PreviewField(75)]
    [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Right")]
    private Sprite unitSprite;
    
    [SerializeField][PreviewField(40)]
    [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Right")]
    private Sprite unitIcon;
    
    [SerializeField] 
    [BoxGroup("Unit")][HorizontalGroup("Unit/Split")][VerticalGroup("Unit/Split/Left")]
    private StatSheet baseStats;

    public Sprite UnitSprite => unitSprite;
    public Sprite UnitIcon => unitIcon;
    public StatSheet BaseStats { get => baseStats; }
}
