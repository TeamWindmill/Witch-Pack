using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    public PathCreator Path;
    [SerializeField] private int coreDamage;
    [SerializeField] private int energyPoints;

    [TabGroup("Agro"), SerializeField, Range(0, 1)] private float agroChance; 
    [TabGroup("Agro"),Tooltip("How many seconds before checking if there is a target to chase"), SerializeField] private float agroInterval; 
    [TabGroup("Agro"),Tooltip("How many seconds before checking if should return to lane"), SerializeField] private float returnInterval; 
    [TabGroup("Agro"),Tooltip("How many seconds before setting a new destination"), SerializeField] private float chaseInterval; 
    [TabGroup("Agro"), SerializeField, Range(0,0.2f)] private float returnChanceModifier; 
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
    public float AgroChance => agroChance;
    public float AgroInterval => agroInterval;
    public float ReturnInterval => returnInterval;
    public float ChaseInterval => chaseInterval;
    public float ReturnChanceModifier => returnChanceModifier;
}
