using UnityEngine;
using Sirenix.OdinInspector;

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
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir)
    {
        owner = shooter;
        refAbility = givenAbility;
        rb.velocity = dir * (speed + shooter.Stats.AbilityProjectileSpeed);
        maxNumberOfHits = baseMaxNumberOfHits + shooter.Stats.AbilityProjectilePenetration;
        //Invoke("Disable", lifeTime);
        //rotate to look at dir given 
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

}
