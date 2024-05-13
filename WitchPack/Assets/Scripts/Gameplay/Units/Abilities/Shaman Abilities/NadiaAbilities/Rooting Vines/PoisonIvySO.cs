using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PoisonIvy")]

public class PoisonIvySO : OffensiveAbilitySO
{
    [SerializeField] private float aoeScale = 1;
    [SerializeField] private float lastingTime;
    [SerializeField] private float poisonDuration;
    [SerializeField] private float poisonTickRate;
    [SerializeField] private int poisonDamage;
    [SerializeField] private Color poisonPopupColor;

    public float PoisonDuration { get => poisonDuration; }
    public float PoisonTickRate { get => poisonTickRate; }
    public int PoisonDamage { get => poisonDamage; }
    public Color PoisonPopupColor { get => poisonPopupColor; }

    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            PoisonIvyMono newIvyPoison = LevelManager.Instance.PoolManager.PoisonIvyPool.GetPooledObject();
            newIvyPoison.Init(caster, this, lastingTime,aoeScale);
            newIvyPoison.transform.position = target.transform.position;
            newIvyPoison.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }

    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}
