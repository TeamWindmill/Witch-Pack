using UnityEngine;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class RicochetSO : MultiShotSO
{
    [SerializeField] private TargetData _ricochetTargetData; 
    [SerializeField] private LayerMask _targetingLayer;
    [SerializeField] private float _bounceRange;
    [SerializeField] private float _bounceSpeed;
    [SerializeField] private float _bounceCurveSpeed;
    
    public TargetData RicochetTargetData => _ricochetTargetData;
    public float BounceRange => _bounceRange;
    public float BounceSpeed => _bounceSpeed;
    public float BounceCurveSpeed => _bounceCurveSpeed;
    public LayerMask TargetingLayer => _targetingLayer;
}