using System.Collections;
using UnityEngine;


public class TargetedShot : MonoBehaviour
{
    //this is essentially a projectile that cannot miss 
    [SerializeField] private float speed;
    [SerializeField] private float maxTravelTime = 2f;
    [SerializeField] private Rigidbody2D rb;
    private BaseAbility ability;
    private BaseUnit owner;
    private Vector3 target;
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, Vector3 target)
    {
        owner = shooter;
        ability = givenAbility;
        rb.velocity = dir * (speed);
        this.target = target;
        StartCoroutine(TravelTimeCountdown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null))
        {
            target.Damageable.GetHit(owner.DamageDealer, ability);
        }
        Disable();
    }


    private IEnumerator TravelTimeCountdown()
    {
        yield return new WaitForSecondsRealtime(maxTravelTime);//lerp at lightning speed if the thing doesnt hit its mark within a given time
        rb.velocity = Vector2.zero;
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, target, counter);
            transform.position = positionLerp;
            counter += Time.deltaTime * 10f;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Disable()
    {
        owner = null;
        ability = null;
        gameObject.SetActive(false);
    }

}
