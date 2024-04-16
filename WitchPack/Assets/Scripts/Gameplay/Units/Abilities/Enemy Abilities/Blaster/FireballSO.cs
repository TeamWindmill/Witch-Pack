using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/EnemyAbilities/Fireball")]
public class FireballSO : OffensiveAbility
{
    [BoxGroup("Fireball")][SerializeField] private float _speed;
    [BoxGroup("AOE Fire")][SerializeField] private float _duration;
    [BoxGroup("AOE Fire")][SerializeField] private float _tickTime;
    [BoxGroup("AOE Fire")][SerializeField] private int _tickAmount;
    [BoxGroup("AOE Fire")][SerializeField] private float _aoeRange;
    [BoxGroup("AOE Fire")][SerializeField] private int _burnDamage;
    [BoxGroup("AOE Fire")][SerializeField] private Color _burnPopupColor;

    public float Duration => _duration;
    public float Speed => _speed;
    public float TickTime => _tickTime;
    public int TickAmount => _tickAmount;
    public float AoeRange => _aoeRange;
    public int BurnDamage => _burnDamage;
    public Color BurnPopupColor => _burnPopupColor;

    public override bool CastAbility(BaseUnit caster)
    {
        var target = caster.ShamanTargetHelper.GetTarget(TargetData);
        if (ReferenceEquals(target, null))
        {
            return false;
        }

        FireballMono fireball = LevelManager.Instance.PoolManager.FireballPool.GetPooledObject();
        fireball.transform.position = caster.CastPos.transform.position;
        fireball.gameObject.SetActive(true);
        fireball.Fire(caster, this, target,_speed);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}