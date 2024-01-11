using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private BaseAbility refAbility;
    private BaseUnit owner;
    [SerializeField] private int baseMaxNumberOfHits;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    private int currentNumberOfHits;
    private int maxNumberOfHits;
    private float lastRate;

    private void Awake()
    {
        GAME_TIME.OnTimeRateChange += ChangeVelocity;
    }

    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir)
    {
        owner = shooter;
        refAbility = givenAbility;
        Rotate(dir);
        lastRate = GAME_TIME.GetCurrentTimeRate;
        rb.velocity = dir * (speed + shooter.Stats.AbilityProjectileSpeed) * lastRate;
        maxNumberOfHits = baseMaxNumberOfHits + shooter.Stats.AbilityProjectilePenetration;
        StartCoroutine(LifeTime());
    }

    private void ChangeVelocity()
    {
        if (gameObject.activeSelf)
        {
            rb.velocity /= lastRate;
            lastRate = GAME_TIME.GetCurrentTimeRate;
            rb.velocity *= lastRate;
        }
    }

    private void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //projectiles can only hit the the layer of the opposite unit type
       
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null))
        {
            target.Damageable.GetHit(owner.DamageDealer, refAbility);
        }
        currentNumberOfHits++;
        if (currentNumberOfHits >= maxNumberOfHits)
        {
            Disable();
        }
    }


    private void Disable()
    {
        owner = null;
        refAbility = null;
        currentNumberOfHits = 0;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (speed <= 1)
        {
            speed = 1;
        }
        if (baseMaxNumberOfHits <= 1)
        {
            baseMaxNumberOfHits = 1;
        }
    }

    private IEnumerator LifeTime()
    {
        float counter = 0f;
        while (counter < lifeTime)
        {
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        Disable();
    }


}
