using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShotSO : OffensiveAbilitySO
{
    [BoxGroup("MultiShot")][SerializeField] protected MultiShotType multiShotType;
    [BoxGroup("MultiShot")][SerializeField] protected int offset;
    [BoxGroup("MultiShot")][SerializeField] protected int speed;
    [BoxGroup("MultiShot")][SerializeField] protected int curveSpeed;
    [BoxGroup("MultiShot")][SerializeField] protected float delay;
    public int Speed => speed;
    public int CurveSpeed => curveSpeed;
    public float Delay => delay;
    public MultiShotType MultiShotType => multiShotType;
    public int Offset => offset;
}

public enum MultiShotType
{
    MultiShot,
    EssenceShot,
    Ricochet
}