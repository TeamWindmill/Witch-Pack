using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class ShamanAutoAttackMono : MonoBehaviour
{
    [SerializeField] protected float initialSpeed;
    [SerializeField] private float maxTravelTime = 2f;
    protected float speed;
    protected BaseAbility ability;
    protected BaseUnit owner;
    protected BaseUnit target;

    public UnityEvent<BaseAbility/*ability cached*/, BaseUnit/*shooter*/, BaseUnit /*target*/> OnShotHit;
    private void Awake()
    {
        SetSpeed(initialSpeed);
    }
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
        if (!ReferenceEquals(target, null) && ReferenceEquals(target, this.target))
        {
            target.Damageable.GetHit(owner.DamageDealer, ability);
            OnShotHit?.Invoke(ability, owner, target);
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

    public void SetSpeed(float vlaue)
    {
        speed = vlaue;
    }


    public virtual void Disable()
    {
        owner = null;
        ability = null;
        SetSpeed(initialSpeed);
        OnShotHit?.RemoveAllListeners();
        gameObject.SetActive(false);
    }

}
