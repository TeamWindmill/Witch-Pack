using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/Toor/MultiShot/MultiShot")]
public class MultiShotSO : OffensiveAbilitySO
{
    [BoxGroup("MultiShot")][SerializeField] protected MultiShotType multiShotType;
    [BoxGroup("MultiShot")][SerializeField] protected int offset;
    [BoxGroup("MultiShot")][SerializeField] protected int speed;
    [BoxGroup("MultiShot")][SerializeField] protected int curveSpeed;
    [BoxGroup("MultiShot")][SerializeField] protected float delay;
    [BoxGroup("MultiShot")][SerializeField] protected float projectilesAmount;
    public int Speed => speed;
    public int CurveSpeed => curveSpeed;
    public float Delay => delay;
    public MultiShotType MultiShotType => multiShotType;
    public int Offset => offset;
    public float ProjectilesAmount => projectilesAmount;
}

public enum MultiShotType
{
    MultiShot,
    EssenceShot,
    Ricochet
}