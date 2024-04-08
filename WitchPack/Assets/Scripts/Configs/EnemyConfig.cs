using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    [HideInInspector] public PathCreator Path;
    [BoxGroup("Enemy")][SerializeField] private int coreDamage;
    [BoxGroup("Enemy")][SerializeField] private int energyPoints;
    [BoxGroup("Enemy")][SerializeField] private GroundEnemyAIConfig _groundEnemyAIConfig;

    public GroundEnemyAIConfig GroundEnemyAIConfig => _groundEnemyAIConfig;
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
    
}
