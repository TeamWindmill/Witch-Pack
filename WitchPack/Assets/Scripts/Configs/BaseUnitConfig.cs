using UnityEngine;

public class BaseUnitConfig : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private StatSheet baseStats;
    [SerializeField] private Sprite unitSprite;
    [SerializeField] private Sprite unitIcon;

    public string Name => name;
    public Sprite UnitSprite => unitSprite;
    public Sprite UnitIcon => unitIcon;
    public StatSheet BaseStats { get => baseStats; }
}
