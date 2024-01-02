using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private ProjectilePool autoAttackPool;

    public ProjectilePool AutoAttackPool { get => autoAttackPool; }
}
