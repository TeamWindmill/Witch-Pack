using PathCreation;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]

public class EnemyConfig : BaseUnitConfig
{
    public PathCreator Path;
    [SerializeField] private int coreDamage;
    public int CoreDamage => coreDamage;
}
