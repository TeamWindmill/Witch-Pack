using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Ricochet", menuName = "Ability/Ricochet")]
public class RicochetSO : MultiShotSO
{
    [BoxGroup("Ricochet")][SerializeField] private TargetData _ricochetTargetData; 
    [BoxGroup("Ricochet")][SerializeField] private LayerMask _targetingLayer;
    [BoxGroup("Ricochet")][SerializeField] private int _bounceAmount;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceRange;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceSpeed;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceCurveSpeed;
    
    public TargetData RicochetTargetData => _ricochetTargetData;
    public int BounceAmount => _bounceAmount;
    public float BounceRange => _bounceRange;
    public float BounceSpeed => _bounceSpeed;
    public float BounceCurveSpeed => _bounceCurveSpeed;
    public LayerMask TargetingLayer => _targetingLayer;
}