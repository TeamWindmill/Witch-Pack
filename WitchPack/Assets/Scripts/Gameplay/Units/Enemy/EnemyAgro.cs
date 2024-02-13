using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAgro
{
    private Random _random = new System.Random();
    private BaseUnit _currentTarget;
    private Enemy _enemy;
    private Vector3 _lastTargetPos;
    private float _agroChance;
    private float _returnChanceModifier;
    private float _timer;
    private bool _chasingTarget;
    private bool _returningToPath;

    private Timer _agroTimer;
    private Timer _chaseTimer;
    private Timer _returnTimer;

    public EnemyAgro(Enemy enemy)
    {
        _chasingTarget = false;
        _enemy = enemy;
        _agroChance = enemy.EnemyConfig.AgroChance;
        _returnChanceModifier = enemy.EnemyConfig.ReturnChanceModifier;
        _agroTimer = GAME_TIME.AddTimer(enemy.EnemyConfig.AgroInterval,TryAgro,true,dontDestroyTimer: true);
        _chaseTimer = GAME_TIME.AddTimer(enemy.EnemyConfig.ChaseInterval,Agro,true,dontDestroyTimer: true);
        _returnTimer = GAME_TIME.AddTimer(enemy.EnemyConfig.ReturnInterval,TryReturn,true,dontDestroyTimer: true);
    }

    public void OnDisable()
    {
        GAME_TIME.RemoveTimer(_agroTimer);
        GAME_TIME.RemoveTimer(_chaseTimer);
        GAME_TIME.RemoveTimer(_returnTimer);
    }

    private void TryAgro()
    {
        if (!_chasingTarget && _enemy.Targeter.HasTarget)
        {
            var chance = _random.NextDouble();
            _currentTarget = _enemy.Targeter.GetClosestTarget();
            var visibilityModifer = 1 - _currentTarget.Stats.Visibility;
            if (chance < _agroChance * visibilityModifer)
            {
                
                _enemy.EnemyMovement.ToggleMove(false);
                _enemy.Movement.ToggleMovement(true);
                _chasingTarget = true;
            }
        }
    }

    private void Agro()
    {
        if (_chasingTarget && !ReferenceEquals(_currentTarget, null))
        {
            if(_lastTargetPos == _currentTarget.transform.position) return;
            _lastTargetPos = _currentTarget.transform.position;
            _enemy.Movement.SetDest(_currentTarget.transform.position);
        }
    }

    private void TryReturn()
    {
        if (_chasingTarget && !ReferenceEquals(_currentTarget, null))
        {
            var chance = _random.NextDouble();
            var returnChance = _returnChanceModifier * Vector3.Distance(_currentTarget.transform.position, _enemy.transform.position);
            if (chance <  returnChance)
            {
                Return();
            }
        }
    }

    private void Return()
    {
        _returningToPath = true;
        _chasingTarget = false;
        _currentTarget = null;
        _enemy.EnemyMovement.ReturnToPath(_enemy.transform.position);
    }

    public void EnemyReturnedToPath() => _returningToPath = false;
}