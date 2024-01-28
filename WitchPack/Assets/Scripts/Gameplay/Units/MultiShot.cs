using UnityEngine;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(45, 90), Tooltip("Angle for the right shot, the left shot will be fired with the same angle only negative")] private float startAngle;

    public override bool CastAbility(BaseUnit caster)
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData);
            if (ReferenceEquals(target, null))
            {
                return false;
            }
            ArchedShot shot = LevelManager.Instance.PoolManager.ArchedShotPool.GetPooledObject();
            shot.transform.position = caster.CastPos.position;
            shot.gameObject.SetActive(true);
            Vector2 dir = target.transform.position - caster.transform.position;

            float angleInRadians = startAngle * Mathf.Deg2Rad;

            float xComponent = Mathf.Cos(angleInRadians);
            float yComponent = Mathf.Sin(angleInRadians);
            Vector2 angleDir = new Vector2(xComponent, yComponent);

            switch (i)
            {
                case 0:
                    shot.Fire(caster, this, dir.normalized, target, Vector3.zero);
                    break;
                case 1:
                    shot.Fire(caster, this, dir.normalized, target, offset);
                    break;
                case 2:
                    shot.Fire(caster, this, dir.normalized, target, -offset);
                    break;
                default:
                    break;
            }
        }
        return true;

    }
}
