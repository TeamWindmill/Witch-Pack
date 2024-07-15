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
        gameObject.SetActive(true);
        SpawnStones(_stoneAmount, timeToSpawn);
    }

    public void SpawnStones(int stoneAmount, float timeToSpawn)
    {
        for (int i = 0; i < stoneAmount; i++)
        {
            var stonePos = CalculatePosition(i, stoneAmount);
            var stone = LevelManager.Instance.PoolManager.FloatingStonesPool.GetPooledObject(stonePos, CalculateRotation(transform.position + stonePos), Vector3.one,_stonesHolder);
            stone.gameObject.SetActive(true);
            _activeStones.Add(stone);
        }
        
        _stonesHolder.Rotate(40,0,0);
        

        for (int i = 0; i < _activeStones.Count; i++)
        {
            _activeStones[i].Init(this,i,i*timeToSpawn);
            //_activeStones[i].transform.LookAt(GameManager.CameraHandler.MainCamera.transform.position);
        }
        _isActive = true;
    }

    private Quaternion CalculateRotation(Vector3 stonePos)
    {
        // vector from this object towards the target location
        Vector3 vectorToTarget = transform.position - stonePos;
        
        // rotate that vector by 90 degrees around the Z axis
        return Quaternion.LookRotation(Vector3.forward,vectorToTarget);
    }

    private Vector3 CalculatePosition(int i, int stoneAmount)
    {
        float angle = i * (360 / stoneAmount);
        float x = math.cos(angle * Mathf.Deg2Rad);
        float y = math.sin(angle * Mathf.Deg2Rad);
        if (_radius == 0) _radius = _debugRadius; //temp for testing
        return new Vector3(x,y ,0) * _radius;
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

    [SerializeField] private FloatingStoneMono[] debugStones;
    [Button]
    public void SetStones()
    {
        int i = 0;
        foreach (var stone in debugStones)
        {
            stone.transform.localPosition = CalculatePosition(i,debugStones.Length);
            stone.transform.rotation = CalculateRotation(stone.transform.position);
            i++;
        }
    }
}