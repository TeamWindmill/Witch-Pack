using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private ProjectilePool testAbilityPool;//unsure yet if well have generic pools or specific ones 
    [SerializeField] private RootingVinesPool rootingVinesPool;
    [SerializeField] private TargetedShotPool shamanAutoAttackPool;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private IndicatorPool inidcatorPool;
    [SerializeField] private ArchedShotPool archedShotPool;
    [SerializeField] private SmokeBombPool smokeBombPool;

    public ProjectilePool TestAbilityPool { get => testAbilityPool; }
    public TargetedShotPool ShamanAutoAttackPool { get => shamanAutoAttackPool; }
    public EnemyPool EnemyPool { get => enemyPool; }
    public IndicatorPool InidcatorPool { get => inidcatorPool; }
    public ArchedShotPool ArchedShotPool { get => archedShotPool; }
    public RootingVinesPool RootingVinesPool { get => rootingVinesPool; }
    public SmokeBombPool SmokeBombPool => smokeBombPool;
}
