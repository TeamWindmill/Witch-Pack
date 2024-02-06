using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAgro 
{
    private Enemy _enemy;
    private BaseUnit _currentTarget;
    private float _agroChance;
    private float _returnChanceModifier;
    private float _agroInterval;
    private float _timer;
    private Random _random = new System.Random();
    private bool _chasingTarget;
    
    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _agroChance = enemy.EnemyConfig.AgroChance;
        _agroInterval = enemy.EnemyConfig.AgroInterval;
        _returnChanceModifier = enemy.EnemyConfig.ReturnChanceModifier;
    }

    public void UpdateAgro()
    {
        if (!_chasingTarget)
        {
            _timer += GAME_TIME.GameDeltaTime;
            if (_timer >= _agroInterval)
            {
                _timer = 0;
                TryAgro();
            }
        }
        else
        {
            _timer += GAME_TIME.GameDeltaTime;
            if (_timer >= _agroInterval)
            {
                _timer = 0;
                Agro();
            }
        }
    }

    private void TryAgro()
    {
        var chance = _random.NextDouble();
        if (chance < _agroChance)
        {
            _currentTarget = _enemy.Targeter.GetClosestTarget();
            _enemy.EnemyMovement.ToggleMove(false);
            _chasingTarget = true;
            Agro();
        }
    }

    private void TryReturn()
    {
        var chance = _random.NextDouble();
        var distance = Vector3.Distance(_currentTarget.transform.position, _enemy.transform.position);
        if (chance < _returnChanceModifier * distance)
        {
            Return();
        }
    }

    private void Agro()
    {
        _enemy.Movement.SetDest(_currentTarget.transform.position);
    }

    private void Return()
    {
        _chasingTarget = false;
        _currentTarget = null;
        
    }
}
