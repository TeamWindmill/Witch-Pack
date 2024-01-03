using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private ProjectilePool testAbilityPool;//unsure yet if well have generic pools or specific ones 
    [SerializeField] private  TargetedShotPool shamanAutoAttackPool;

    public ProjectilePool TestAbilityPool { get => testAbilityPool; }
    public TargetedShotPool ShamanAutoAttackPool { get => shamanAutoAttackPool; }
}
