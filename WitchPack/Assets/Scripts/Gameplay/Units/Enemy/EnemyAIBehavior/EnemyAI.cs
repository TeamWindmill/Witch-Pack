using Systems.StateMachine;

public class EnemyAI: BaseStateMachine<EnemyAI>
{
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