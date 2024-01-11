using System.Collections;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;


public class TargetedShot : MonoBehaviour
{
    //this is essentially a projectile that cannot miss 
    [SerializeField] private float speed;
    [SerializeField] private float maxTravelTime = 2f;
    [SerializeField] private Rigidbody2D rb;
    private BaseAbility ability;
    private BaseUnit owner;
    private Transform target;
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, Transform target)
    {
        owner = shooter;
        ability = givenAbility;
        this.target = target;
        Rotate(dir);
        //rb.velocity = dir * (speed);
        StartCoroutine(TravelTimeCountdown());
    }

    private void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
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
        //yield return new WaitForSecondsRealtime(maxTravelTime);//lerp at lightning speed if the thing doesnt hit its mark within a given time
        rb.velocity = Vector2.zero;
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, target.position, counter);
            transform.position = positionLerp;
            counter += GAME_TIME.GameDeltaTime * speed;
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
