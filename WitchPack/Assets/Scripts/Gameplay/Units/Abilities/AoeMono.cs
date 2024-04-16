using UnityEngine;

public class AoeMono : MonoBehaviour
{
    private float ringLastingTime;
    private float elapsedTime;
    [SerializeField] private GroundColliderTargeter groundColliderTargeter;

    protected CastingAbility _ability;
    protected BaseUnit _owner;
    public virtual void Init(BaseUnit owner, CastingAbility ability, float lastingTime)
    {
        ringLastingTime = lastingTime;
        _owner = owner;
        _ability = ability;
        groundColliderTargeter.OnTargetAdded += OnTargetEntered;
    }
    protected virtual void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= ringLastingTime)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
            groundColliderTargeter.OnTargetAdded -= OnTargetEntered;
        }
    }

    
    protected virtual void OnTargetEntered(GroundCollider collider)
    {
        if(collider.Unit is Enemy enemy)
        {
            OnEnemyEnter(enemy);
        }
        else if (collider.Unit is Shaman shaman)
        {
            OnShamanEnter(shaman);
        }
    }

    protected virtual void OnShamanEnter(Shaman shaman)
    {
        
    }
    protected virtual void OnEnemyEnter(Enemy enemy)
    {
        
    }
}