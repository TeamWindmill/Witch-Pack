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
    [SerializeField] private ParticleSystem _marksmanPiercingShot;
    [SerializeField] private ParticleSystem _experiencedHunterPiercingShot;
    [SerializeField] private ParticleSystem _quickShotPiercingShot;
    [SerializeField] private Rigidbody2D _rb;
    
    [Header("variables")]
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    
    private int _baseMaxNumberOfHits;
    private int _currentNumberOfHits;
    private int _maxNumberOfHits;
    private OffensiveAbility refAbility;
    private BaseUnit _owner;
    private Vector2 _dir;

    private void OnEnable()
    {
        GAME_TIME.OnTimeRateChange += ChangeVelocity;
        _basicPiercingShot.gameObject.SetActive(false);
        _marksmanPiercingShot.gameObject.SetActive(false);
        _experiencedHunterPiercingShot.gameObject.SetActive(false);
        _quickShotPiercingShot.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        GAME_TIME.OnTimeRateChange -= ChangeVelocity;
    }
    public void Fire(BaseUnit shooter, OffensiveAbility givenAbility, Vector2 dir, int basePen = 0, bool includePenStat = false)
    {
        EnableVisuals(givenAbility.OffensiveAbilityConfig);
        _owner = shooter;
        refAbility = givenAbility;
        _dir = dir;
        Rotate(dir);
        _rb.velocity = (_speed) * GAME_TIME.TimeRate * _dir;
        if (includePenStat)
        {
            _maxNumberOfHits = _baseMaxNumberOfHits + shooter.Stats.AbilityProjectilePenetration + basePen;
        }
        else
        {
            _maxNumberOfHits = _baseMaxNumberOfHits + basePen;
        }
        StartCoroutine(LifeTime());
    }

    private void EnableVisuals(AbilitySO givenAbilitySo)
    {
        switch (givenAbilitySo.Name)
        {
            case BASIC_PIERCING_SHOT:
                _basicPiercingShot.gameObject.SetActive(true);
                break;
            case MARKSMAN:
                _marksmanPiercingShot.gameObject.SetActive(true);
                break;
            case EXPERIENCED_HUNTER:
                _experiencedHunterPiercingShot.gameObject.SetActive(true);
                break;
            case QUICK_SHOT:
                _quickShotPiercingShot.gameObject.SetActive(true);
                break;
        }
    }

    private void ChangeVelocity()
    {
        if (!gameObject.activeSelf) return;
        
        _rb.velocity = _speed * GAME_TIME.TimeRate * _dir;
    }

    private void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null) && !ReferenceEquals(_owner, null))
        {
            target.Damageable.GetHit(_owner.DamageDealer, refAbility);
        }
        _currentNumberOfHits++;
        if (_currentNumberOfHits >= _maxNumberOfHits)
        {
            Disable();
        }
    }


    private void Disable()
    {
        _owner = null;
        refAbility = null;
        _currentNumberOfHits = 0;
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_speed <= 1)
        {
            _speed = 1;
        }
        if (_baseMaxNumberOfHits <= 1)
        {
            _baseMaxNumberOfHits = 1;
        }
    }

    private IEnumerator LifeTime()
    {
        float counter = 0f;
        while (counter < _lifeTime)
        {
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        Disable();
    }


}
