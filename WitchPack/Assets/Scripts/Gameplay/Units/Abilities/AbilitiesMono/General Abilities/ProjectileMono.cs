using System;
using System.Collections;
using Systems.ObjectPool;
using UnityEngine;

public class ProjectileMono : PoolableObject
{
    protected float _speed;
    protected OffensiveAbility Ability;
    protected BaseUnit _owner;
    protected IDamagable _target;

    public event Action<CastingAbility, BaseUnit, IDamagable> OnShotHit;

    public virtual void Fire(BaseUnit caster, OffensiveAbility givenAbility, IDamagable target,float speed)
    {
        _owner = caster;
        Ability = givenAbility;
        _target = target;
        SetSpeed(speed);
        Vector2 dir = (target.GameObject.transform.position - caster.transform.position).normalized;
        Rotate(dir);
        StartCoroutine(TravelTimeCountdown());
    }

    private void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.GetComponent<IDamagable>() ?? collision.GetComponentInParent<IDamagable>();
        if (!ReferenceEquals(target, null) && ReferenceEquals(target, _target))
        {
            OnTargetHit(target);
            OnShotHit?.Invoke(Ability, _owner, target);
            Disable();
        }
    }

    protected virtual void OnTargetHit(IDamagable target)
    {
        target.Damageable.GetHit(_owner.DamageDealer, Ability);
    }
    private IEnumerator TravelTimeCountdown()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, _target.GameObject.transform.position, counter);
            transform.position = positionLerp;
            counter += GAME_TIME.GameDeltaTime * _speed;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        transform.position = _target.GameObject.transform.position;
        //Disable?
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }


    public virtual void Disable()
    {
        _owner = null;
        Ability = null;
        _target = null;
        OnShotHit = null;
        gameObject.SetActive(false);
    }

    public override PoolableObject FactoryMethod()
    {
        //return Instantiate();
        return null;
    }

    public override void TurnOnCallback(PoolableObject obj)
    {
        throw new NotImplementedException();
    }

    public override void TurnOffCallback(PoolableObject obj)
    {
        throw new NotImplementedException();
    }
}