using System;
using System.Collections;
using UnityEngine;

public class ProjectileMono : MonoBehaviour
{
    [SerializeField] protected float initialSpeed;
    protected float speed;
    protected CastingAbility ability;
    protected BaseUnit owner;
    protected IDamagable _target;

    public event Action<CastingAbility, BaseUnit, IDamagable> OnShotHit;
    protected virtual void Awake()
    {
        SetSpeed(initialSpeed);
    }
    public virtual void Fire(BaseUnit caster, CastingAbility givenAbility, IDamagable target)
    {
        Vector2 dir = (target.GameObject.transform.position - caster.transform.position).normalized;
        owner = caster;
        ability = givenAbility;
        _target = target;
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
            OnShotHit?.Invoke(ability, owner, target);
            Disable();
        }
    }

    protected virtual void OnTargetHit(IDamagable target)
    {
        target.Damageable.GetHit(owner.DamageDealer, ability);
    }
    private IEnumerator TravelTimeCountdown()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, _target.GameObject.transform.position, counter);
            transform.position = positionLerp;
            counter += GAME_TIME.GameDeltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        transform.position = _target.GameObject.transform.position;
        //Disable?
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }


    public virtual void Disable()
    {
        owner = null;
        ability = null;
        _target = null;
        SetSpeed(initialSpeed);
        OnShotHit = null;
        gameObject.SetActive(false);
    }
}