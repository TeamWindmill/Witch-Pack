using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PiercingShotMono : MonoBehaviour
{
    private const string BASIC_PIERCING_SHOT = "Piercing Shot";
    private const string MARKSMAN = "Marksman";
    private const string EXPERIENCED_HUNTER = "Experienced Hunter";
    private const string QUICK_SHOT = "Quick Shot";
    
    [Header("refs")]
    [SerializeField] private ParticleSystem _basicPiercingShot;
    [SerializeField] private ParticleSystem _MarksmanPiercingShot;
    [SerializeField] private ParticleSystem _ExperiencedHunterPiercingShot;
    [SerializeField] private ParticleSystem _QuickShotPiercingShot;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("variables")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    
    private int baseMaxNumberOfHits;
    private int currentNumberOfHits;
    private int maxNumberOfHits;
    private float lastRate;
    private BaseAbility refAbility;
    private BaseUnit owner;

    private void OnEnable()
    {
        GAME_TIME.OnTimeRateChange += ChangeVelocity;
        _basicPiercingShot.gameObject.SetActive(false);
        _MarksmanPiercingShot.gameObject.SetActive(false);
        _ExperiencedHunterPiercingShot.gameObject.SetActive(false);
        _QuickShotPiercingShot.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        GAME_TIME.OnTimeRateChange -= ChangeVelocity;
    }
    public void Fire(BaseUnit shooter, BaseAbility givenAbility, Vector2 dir, int basePen = 0, bool includePenStat = false)
    {
        EnableVisuals(givenAbility);
        owner = shooter;
        refAbility = givenAbility;
        Rotate(dir);
        lastRate = GAME_TIME.GetCurrentTimeRate;
        rb.velocity = dir * (speed) * lastRate;
        if (includePenStat)
        {
            maxNumberOfHits = baseMaxNumberOfHits + shooter.Stats.AbilityProjectilePenetration + basePen;
        }
        else
        {
            maxNumberOfHits = baseMaxNumberOfHits + basePen;
        }
        StartCoroutine(LifeTime());
    }

    private void EnableVisuals(BaseAbility givenAbility)
    {
        switch (givenAbility.Name)
        {
            case BASIC_PIERCING_SHOT:
                _basicPiercingShot.gameObject.SetActive(true);
                break;
            case MARKSMAN:
                _MarksmanPiercingShot.gameObject.SetActive(true);
                break;
            case EXPERIENCED_HUNTER:
                _ExperiencedHunterPiercingShot.gameObject.SetActive(true);
                break;
            case QUICK_SHOT:
                _QuickShotPiercingShot.gameObject.SetActive(true);
                break;
        }
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
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null) && !ReferenceEquals(owner, null))
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
