using PathCreation;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    public PathCreator Path;
    [SerializeField] private int coreDamage;
    [SerializeField] private int energyPoints;

    [Header("Agro")] 
    [SerializeField, Range(0, 1)] private float agroChance; 
    [SerializeField] private float agroInterval; 
    [SerializeField, Range(0,0.2f)] private float returnChanceModifier; 
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
    public float AgroChance => agroChance;
    public float AgroInterval => agroInterval;
    public float ReturnChanceModifier => returnChanceModifier;
}
