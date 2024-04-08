using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Systems.StateMachine;

public class EnemyAI : BaseStateMachine<EnemyAI>
{
    public State<EnemyAI> ActiveState => _activeState;
    public Enemy Enemy { get; private set; }
    public BaseUnit CurrentTarget { get; private set; }
    public float AgroChance => _agroChance;
    public TargetData TargetData => _targetData;
    public float ReturnChance => _returnChance;
    public float DistanceModifier => _distanceModifier;
    
    
    private float _agroChance;
    private TargetData _targetData;
    private float _returnChance;
    private float _distanceModifier;

    public void Init(Enemy owner)
    {
        Enemy = owner;
        var config = owner.EnemyConfig.GroundEnemyAIConfig;
        _agroChance = config.AgroChance;
        _targetData = config.TargetData;
        _returnChance = config.ReturnChance;
        _distanceModifier = config.DistanceModifier;
        BaseInit(config.EnemyStates);
    }
    public void OnDisable()
    {
        if(_activeState != null) _activeState.Exit(this);
    }

    public void SetTarget(BaseUnit target)
    {
        CurrentTarget = target;
    }
}