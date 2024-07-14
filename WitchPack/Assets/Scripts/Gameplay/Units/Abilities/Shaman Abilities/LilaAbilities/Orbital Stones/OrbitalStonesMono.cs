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

    [SerializeField] private Transform _stonesHolder;
    [SerializeField] private float _debugRadius;
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

        _stoneAmount = Ability.GetAbilityStatIntValue(AbilityStatType.ProjectilesAmount);
        var angleDiff = 360 / _stoneAmount;

        var timeToSpawn = angleDiff / (Ability.GetAbilityStatValue(AbilityStatType.Speed));
        SpawnStones(_stoneAmount, timeToSpawn);
    }

    public void SpawnStones(int stoneAmount, float timeToSpawn)
    {
        for (int i = 0; i < stoneAmount; i++)
        {
            var stone = LevelManager.Instance.PoolManager.FloatingStonesPool.GetPooledObject(CalculatePosition(i, stoneAmount), Quaternion.identity,Vector3.one,_stonesHolder);
            stone.Init(this, i, i * timeToSpawn);
            _activeStones.Add(stone);
        }
        gameObject.SetActive(true);
        _isActive = true;
    }

    private Vector3 CalculatePosition(int i, int stoneAmount)
    {
        float angle = (float)i * (2 * Mathf.PI / stoneAmount);
        float posX = Mathf.Cos(angle) * _radius;
        float posY = Mathf.Sin(angle) * _radius;
        return new Vector3(posX, posY, 0);
        // var angle = i * (360 / stoneAmount);
        // var posX = MathF.Sin(angle * Mathf.Deg2Rad);
        // var posY = MathF.Cos(angle * Mathf.Deg2Rad)/_ellipseScale;
        // var offset = new Vector3(posX, posY, 0) * _radius;
        // return offset;
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
        _stonesHolder.Rotate(0, 0, _angularSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _debugRadius);
    }
}