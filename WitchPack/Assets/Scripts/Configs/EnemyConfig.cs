using PathCreation;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    [HideInInspector] public PathCreator Path;
    [SerializeField] private int coreDamage;
    [SerializeField] private int energyPoints;
    [SerializeField] private EnemyAIConfig enemyAIConfig;

    public EnemyAIConfig EnemyAIConfig => enemyAIConfig;
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
    
}
