using System;
using System.Collections;
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
    public BaseUnit Owner { get; private set; }
    public OrbitalStones Ability { get; private set; }

    private List<FloatingStoneMono> _activeStones = new();
    private float _angularSpeed;
    private float _radius;
    private float _ellipseScale;
    private bool _isActive;
    private int _stoneAmount;


    public void Init(BaseUnit owner, OrbitalStones ability)
    {
        Owner = owner;
        Ability = ability;
        _ellipseScale = ability.Config.EllipseScale;
        _angularSpeed = ability.GetAbilityStatValue(AbilityStatType.Speed);
        _radius = ability.GetAbilityStatValue(AbilityStatType.Size);
        gameObject.SetActive(true);

        _stoneAmount = Ability.GetAbilityStatIntValue(AbilityStatType.ProjectilesAmount);
        var angleDiff = 360 / _stoneAmount;

        var timeToSpawn = angleDiff / (Ability.GetAbilityStatValue(AbilityStatType.Speed));
        StartCoroutine(SpawnStones(_stoneAmount, timeToSpawn));

        _isActive = true;
    }

    public IEnumerator SpawnStones(int stoneAmount, float timeInterval)
    {
        for (int i = 0; i < stoneAmount; i++)
        {
            SpawnStone(i);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public void SpawnStone(int index)
    {
        var stone = LevelManager.Instance.PoolManager.FloatingStonesPool.GetPooledObject();
        stone.Init(this,index);
        _activeStones.Add(stone);
    }

    private int stoneCounter;

    public void OnStoneDisable()
    {
        stoneCounter++;
        if (stoneCounter == _stoneAmount)
        {
            _activeStones.Clear();
            gameObject.SetActive(false);
            _isActive = false;
            stoneCounter = 0;
        }
    }

    private void Update()
    {
        if (!_isActive) return;
        transform.position = Owner.transform.position;
    }
}