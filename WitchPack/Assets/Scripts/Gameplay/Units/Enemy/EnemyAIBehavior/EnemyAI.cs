using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Systems.StateMachine;

public class EnemyAI: BaseStateMachine<EnemyAI>
{
    public State<EnemyAI> ActiveState => _activeState;
    public Enemy Enemy { get; private set; }
    public EnemyAIConfig Config { get; private set; }

    public void Init(Enemy owner)
    {
        Enemy = owner;
        Config = owner.EnemyConfig.EnemyAIConfig;
        BaseInit(Config.EnemyStates);
    }
    public void OnDisable()
    {
        if(_activeState != null) _activeState.Exit(this);
    }
}