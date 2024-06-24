using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tools.Lerp;
using Unity.Mathematics;
using UnityEngine;

public class OrbitalStonesMono : MonoBehaviour
{
    public float AngularSpeed => _angularSpeed;
    public float Radius => _radius;
    public float EllipseScale => _ellipseScale;
    public BaseUnit Owner{ get; private set; }
    public OrbitalStones Ability { get; private set; }
    
    [SerializeField] private OrbitalStone _orbitalStonePrefab;

    private List<OrbitalStone> _activeStones = new();
    private float _angularSpeed;
    private float _radius;
    private float _ellipseScale;
    private bool _isActive;
    
    
    public void Init(BaseUnit owner, OrbitalStones ability,int stoneAmount)
    {
        Owner = owner;
        Ability = ability;
        _ellipseScale = ability.Config.EllipseScale;
        _angularSpeed = ability.GetAbilityStatValue(AbilityStatType.Speed);
        _radius = ability.GetAbilityStatValue(AbilityStatType.Size);
        
        var angleDiff = 360 / (stoneAmount);
        for (int i = 0; i < stoneAmount; i++)
        {
            var stone = LevelManager.Instance.PoolManager.FloatingStonesPool.GetPooledObject();
            stone.Init(this,angleDiff * (i));
            _activeStones.Add(stone);
        }

        TimerManager.AddTimer(ability.GetAbilityStatValue(AbilityStatType.Duration), DisableStones);

    }
    
    public void DisableStones()
    {
        foreach (var stone in _activeStones)
        {
            stone.Disable();
        }
        _activeStones.Clear();
    }

    private void Update()
    {
        if (!_isActive) return;
        transform.position = Owner.transform.position;
    }
}