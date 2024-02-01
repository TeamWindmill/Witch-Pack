using PathCreation;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    public PathCreator Path;
    [SerializeField] private int coreDamage;
    [SerializeField] private int energyPoints;
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
}
