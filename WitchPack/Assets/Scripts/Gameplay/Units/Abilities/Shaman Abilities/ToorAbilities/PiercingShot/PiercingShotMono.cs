using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PiercingShotMono : MonoBehaviour
{
    
    [Header("refs")]
    [SerializeField] private ParticleSystem _basicPiercingShot;
    [SerializeField] private ParticleSystem _marksmanPiercingShot;
    [SerializeField] private ParticleSystem _experiencedHunterPiercingShot;
    [SerializeField] private ParticleSystem _quickShotPiercingShot;
    [SerializeField] private Rigidbody2D _rb;
    
    
    private float _speed => _ability.GetAbilityStatValue(AbilityStatType.Speed);
    private float _lifeTime => _ability.GetAbilityStatValue(AbilityStatType.Duration);

    private readonly int _baseMaxNumberOfHits = 1;
    private int _currentNumberOfHits;
    private int _maxNumberOfHits;
    private PiercingShot _ability;
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
    public void Fire(BaseUnit shooter, PiercingShot givenAbility, Vector2 dir, int basePen = 0, bool includePenStat = false)
    {
        EnableVisuals(givenAbility.PiercingShotConfig);
        _owner = shooter;
        _ability = givenAbility;
        _dir = dir;
        Rotate(dir);
        _rb.velocity = (_speed) * GAME_TIME.TimeRate * _dir;
        if (includePenStat)
        {
            _maxNumberOfHits = _baseMaxNumberOfHits + _ability.ProjectilePenetration + basePen;
        }
        else
        {
            _maxNumberOfHits = _baseMaxNumberOfHits + basePen;
        }
        StartCoroutine(LifeTime());
    }

    private void EnableVisuals(PiercingShotSO givenAbilitySo)
    {
        switch (givenAbilitySo.Type)
        {
            case PiercingShotType.PiercingShot:
                _basicPiercingShot.gameObject.SetActive(true);
                break;
            case PiercingShotType.QuickShot:
                _quickShotPiercingShot.gameObject.SetActive(true);
                break;
            case PiercingShotType.Marksman:
                _marksmanPiercingShot.gameObject.SetActive(true);
                break;
            case PiercingShotType.ExperiencedHunter:
                _experiencedHunterPiercingShot.gameObject.SetActive(true);
                break;
        }
    }

    private void ChangeVelocity(float newTime)
    {
        if (!gameObject.activeSelf) return;
        
        _rb.velocity = _speed * newTime * _dir;
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
            target.Damageable.GetHit(_owner.DamageDealer, _ability);
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
        _ability = null;
        _currentNumberOfHits = 0;
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
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
