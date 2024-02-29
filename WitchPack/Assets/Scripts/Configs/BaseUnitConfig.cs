using UnityEngine;

public class BaseUnitConfig : BaseConfig
{
    [SerializeField] private StatSheet baseStats;
    [SerializeField] private Sprite unitSprite;
    [SerializeField] private Sprite unitIcon;

    public string Name => name;
    public Sprite UnitSprite => unitSprite;
    public Sprite UnitIcon => unitIcon;
    public StatSheet BaseStats { get => baseStats; }
}
