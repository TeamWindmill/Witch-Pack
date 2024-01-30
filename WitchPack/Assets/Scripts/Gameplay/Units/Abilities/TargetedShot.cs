using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TargetedShot : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float maxTravelTime = 2f;
    protected BaseAbility ability;
    protected BaseUnit owner;
    protected BaseUnit target;

    public UnityEvent<BaseAbility/*ability cached*/, BaseUnit/*shooter*/> OnShotHit;

    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, BaseUnit target)
    {
        owner = shooter;
        ability = givenAbility;
        this.target = target;
        Rotate(dir);
        StartCoroutine(TravelTimeCountdown());
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
        Disable();
    }

    protected virtual void Disable()
    {
        owner = null;
        ability = null;
        OnShotHit?.RemoveAllListeners();
        gameObject.SetActive(false);
    }

}
