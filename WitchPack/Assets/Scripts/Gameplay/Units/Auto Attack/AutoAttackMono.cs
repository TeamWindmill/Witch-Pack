using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class AutoAttackMono : MonoBehaviour
{
    [SerializeField] protected float initialSpeed;
    private float speed;
    private AutoAttack ability;
    private BaseUnit owner;
    private IDamagable _target;

    public UnityEvent<AutoAttack/*ability cached*/, BaseUnit/*shooter*/, IDamagable /*target*/> OnShotHit;
    private void Awake()
    {
        SetSpeed(initialSpeed);
    }
    public void Fire(BaseUnit shooter, AutoAttack givenAbility, Vector2 dir, IDamagable target)
    {
        owner = shooter;
        ability = givenAbility;
        _target = target;
        Rotate(dir);
        StartCoroutine(TravelTimeCountdown());
    }

    private void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.GetComponent<IDamagable>() ?? collision.GetComponentInParent<IDamagable>();
        if (!ReferenceEquals(target, null) && ReferenceEquals(target, _target))
        {
            switch (target)
            {
                case CoreTemple:
                    target.Damageable.TakeFlatDamage(ability.CoreDamage);
                    break;
                case BaseUnit:
                    target.Damageable.GetHit(owner.DamageDealer, ability);
                    break;
            }
            OnShotHit?.Invoke(ability, owner, target);
            Disable();
        }
    }


    private IEnumerator TravelTimeCountdown()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, _target.GameObject.transform.position, counter);
            transform.position = positionLerp;
            counter += GAME_TIME.GameDeltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        transform.position = _target.GameObject.transform.position;
       

    }

    public void SetSpeed(float value)
    {
        speed = value;
    }


    public void Disable()
    {
        owner = null;
        ability = null;
        _target = null;
        SetSpeed(initialSpeed);
        OnShotHit?.RemoveAllListeners();
        gameObject.SetActive(false);
    }

}
