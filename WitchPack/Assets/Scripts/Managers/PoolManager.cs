using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PiercingShotPool _piercingShotPool;
    [SerializeField] private RootingVinesPool rootingVinesPool;
    [SerializeField] private AutoAttackPool shamanAutoAttackPool;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private IndicatorPool inidcatorPool;
    [SerializeField] private MultiShotPool _multiShotPool;
    [SerializeField] private RicochetPool _ricochetPool;
    [SerializeField] private SmokeBombPool smokeBombPool;
    [SerializeField] private HighImpactPool highImpactPool;
    [SerializeField] private PoisonIvyPool poisonIvyPool;
    [SerializeField] private HealingWeedsPool healingWeedsPool;
    [SerializeField] private FireballPool _fireballPool;
    [SerializeField] private AoeFirePool _aoeFirePool;
    [SerializeField] private AftershockPool _aftershockPool;
    [SerializeField] private OrbitalStonesPool _orbitalStonesPool;
    [SerializeField] private FloatingStonesPool _floatingStonesPool;

    #region Pointers

    public AoeFirePool AoeFirePool => _aoeFirePool;
    public FireballPool FireballPool => _fireballPool;
    public PiercingShotPool PiercingShotPool { get => _piercingShotPool; }
    public RicochetPool RicochetPool => _ricochetPool;
    public AutoAttackPool ShamanAutoAttackPool { get => shamanAutoAttackPool; }
    public EnemyPool EnemyPool { get => enemyPool; }
    public IndicatorPool InidcatorPool { get => inidcatorPool; }
    public MultiShotPool MultiShotPool { get => _multiShotPool; }
    public RootingVinesPool RootingVinesPool { get => rootingVinesPool; }
    public SmokeBombPool SmokeBombPool => smokeBombPool;
    public HighImpactPool HighImpactPool => highImpactPool;
    public PoisonIvyPool PoisonIvyPool { get => poisonIvyPool; }
    public HealingWeedsPool HealingWeedsPool { get => healingWeedsPool; }
    public AftershockPool AftershockPool => _aftershockPool;
    public OrbitalStonesPool OrbitalStonesPool => _orbitalStonesPool;
    public FloatingStonesPool FloatingStonesPool => _floatingStonesPool;

    #endregion
    private void OnValidate()
    {
        _piercingShotPool ??= GetComponentInChildren<PiercingShotPool>();
        rootingVinesPool ??= GetComponentInChildren<RootingVinesPool>();
        shamanAutoAttackPool ??= GetComponentInChildren<AutoAttackPool>();
        enemyPool ??= GetComponentInChildren<EnemyPool>();
        inidcatorPool ??= GetComponentInChildren<IndicatorPool>();
        _multiShotPool ??= GetComponentInChildren<MultiShotPool>();
        _ricochetPool ??= GetComponentInChildren<RicochetPool>();
        smokeBombPool ??= GetComponentInChildren<SmokeBombPool>();
        highImpactPool ??= GetComponentInChildren<HighImpactPool>();
        poisonIvyPool ??= GetComponentInChildren<PoisonIvyPool>();
        healingWeedsPool ??= GetComponentInChildren<HealingWeedsPool>();
        _fireballPool ??= GetComponentInChildren<FireballPool>();
        _aoeFirePool ??= GetComponentInChildren<AoeFirePool>();
        _aftershockPool ??= GetComponentInChildren<AftershockPool>();
    }
}
