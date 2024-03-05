using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnitConfig : BaseConfig
{
    [SerializeField] private Sprite unitSprite;
    
    [SerializeField] private Sprite unitIcon;
    
    [SerializeField] 
    private StatSheet baseStats;

    public Sprite UnitSprite => unitSprite;
    public Sprite UnitIcon => unitIcon;
    public StatSheet BaseStats { get => baseStats; }
}
