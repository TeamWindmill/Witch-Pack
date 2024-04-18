using System.Collections.Generic;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    [HideInInspector] public PathCreator Path;
    [BoxGroup("Enemy")][SerializeField] private int coreDamage;
    [BoxGroup("Enemy")][SerializeField] private int energyPoints;
    [BoxGroup("Enemy")][SerializeField] private List<BaseAbility> _abilities;
    [BoxGroup("Enemy")][SerializeField] private EnemyAIConfig _enemyAIConfig;

    public EnemyAIConfig EnemyAIConfig => _enemyAIConfig;
    public int CoreDamage => coreDamage;
    public int EnergyPoints => energyPoints;
    public List<BaseAbility> Abilities => _abilities;
}
