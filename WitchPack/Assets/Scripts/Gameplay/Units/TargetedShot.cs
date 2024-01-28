using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetedShot : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float maxTravelTime = 2f;
    protected BaseAbility ability;
    protected BaseUnit owner;
    protected BaseUnit target;
    protected int ricochet;
    protected float ricochetRange;
    private int hits;
    private List<BaseUnit> hitTargets;
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, BaseUnit target)
    {
        owner = shooter;
        ability = givenAbility;
        this.target = target;
        Rotate(dir);
        StartCoroutine(TravelTimeCountdown());
    }

    public void SetRicochet(int numberOfJumps, float ricochetRange)
    {
        ricochet = numberOfJumps;
        this.ricochetRange = ricochetRange;
        hitTargets = new List<BaseUnit>();
    }

    protected void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null) && !ReferenceEquals(ability, null) && ReferenceEquals(target, this.target))
        {
            target.Damageable.GetHit(owner.DamageDealer, ability);
         /*   if (ricochet > 0)
            {
                hitTargets.Add(target);
                hits++;
                if (hits <= ricochet)
                {
                    target = owner.TargetHelper.GetTarget(owner.Targeter.GetAvailableTargets(transform.position, ricochetRange), ability.TargetData, hitTargets);
                    if (!ReferenceEquals(target, null))
                    {
                        StartCoroutine(TravelTimeCountdown());
                        return;
                    }
                }
            }*/
            Disable();
        }
    }


    private IEnumerator TravelTimeCountdown()
    {
        //yield return new WaitForSecondsRealtime(maxTravelTime);
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, target.transform.position, counter);
            transform.position = positionLerp;
            counter += GAME_TIME.GameDeltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        transform.position = target.transform.position;
        /*if (target.Damageable.CurrentHp >0)
        {
            Disable();
        }*/
    }

    protected virtual void Disable()
    {
        owner = null;
        ability = null;
        ricochet = 0;
        hits = 0;
        hitTargets?.Clear();
        gameObject.SetActive(false);
    }

}
