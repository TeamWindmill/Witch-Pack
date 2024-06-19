using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AffectedByUnitsStatPassive", menuName = "Ability/Passive/AffectedByUnitsStatPassive")]
public class AffectedByUnitsStatPassiveSO : PassiveSO
{
    [Header("Affected By Units Stat Passive")] [SerializeField]
    private StatValue[] _statsIncrease;

    [SerializeField] private bool _affectedByEnemies;

    [SerializeField] [ShowIf(nameof(_affectedByEnemies))]
    private bool _affectedByEnemiesWithStatusEffect;

    [SerializeField] [ShowIf(nameof(_affectedByEnemiesWithStatusEffect))]
    private StatusEffectType _enemyStatusEffect;

    [SerializeField] private bool _affectedByShamans;

    [SerializeField] [ShowIf(nameof(_affectedByShamans))]
    private bool _affectedByShamansWithStatusEffect;

    [SerializeField] [ShowIf(nameof(_affectedByShamansWithStatusEffect))]
    private StatusEffectType _shamanStatusEffect;

    public StatValue[] StatsIncrease => _statsIncrease;
    public bool AffectedByEnemies => _affectedByEnemies;

    public bool AffectedByEnemiesWithStatusEffect => _affectedByEnemiesWithStatusEffect;

    public StatusEffectType EnemyStatusEffect => _enemyStatusEffect;
    public bool AffectedByShamans => _affectedByShamans;

    public bool AffectedByShamansWithStatusEffect => _affectedByShamansWithStatusEffect;

    public StatusEffectType ShamanStatusEffect => _shamanStatusEffect;
}