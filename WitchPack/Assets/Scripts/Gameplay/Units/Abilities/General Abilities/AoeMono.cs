using UnityEngine;

public class AoeMono : MonoBehaviour
{
    private float ringLastingTime;
    private float elapsedTime;
    [SerializeField] private Transform _holder;
    [SerializeField] private Transform _rangeVisuals;
    [SerializeField] private GroundColliderTargeter _groundColliderTargeter;

    protected CastingAbility _ability;
    protected BaseUnit _owner;
    public virtual void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
    {
        ringLastingTime = lastingTime;
        _owner = owner;
        _ability = ability;
        _holder.transform.localScale = new Vector3(aoeRange,aoeRange,aoeRange);
        _rangeVisuals.transform.localScale = new Vector3(aoeRange,aoeRange,aoeRange);
        _groundColliderTargeter.OnTargetAdded += (c) => TargetInteract(c,true);
        _groundColliderTargeter.OnTargetLost += (c) => TargetInteract(c,false);
    }
    protected virtual void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= ringLastingTime)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
        }
    }

    
    private void TargetInteract(GroundCollider collider, bool enter)
    {
        if(collider.Unit is Enemy enemy)
        {
            if (enter) OnEnemyEnter(enemy);
            else OnEnemyExit(enemy);

        }
        else if (collider.Unit is Shaman shaman)
        {
            if (enter) OnShamanEnter(shaman);
            else OnShamanExit(shaman);
        }
    }

    protected virtual void OnShamanEnter(Shaman shaman)
    {
        
    }
    protected virtual void OnShamanExit(Shaman shaman)
    {
        
    }
    protected virtual void OnEnemyEnter(Enemy enemy)
    {
        
    }
    protected virtual void OnEnemyExit(Enemy enemy)
    {
        
    }
}