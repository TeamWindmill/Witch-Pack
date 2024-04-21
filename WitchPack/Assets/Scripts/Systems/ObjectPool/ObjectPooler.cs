using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems.ObjectPool
{
    public class ObjectPooler
    {
        private readonly List<PoolableObject> _currentStock;
        private readonly bool _isDynamic;
        private readonly Func<PoolableObject> _factoryMethod;
        private readonly Action<PoolableObject> _turnOnCallback;
        private readonly Action<PoolableObject> _turnOffCallback;

        public ObjectPooler(PoolableObject poolable)
        {
            _factoryMethod = poolable.FactoryMethod;
            _isDynamic = poolable.IsDynamic;

            _turnOffCallback = poolable.TurnOffCallback;
            _turnOnCallback = poolable.TurnOnCallback;

            _currentStock = new List<PoolableObject>();

            for (var i = 0; i < poolable.InitialStock; i++)
            {
                var obj = _factoryMethod();
                _turnOffCallback(obj);
                _currentStock.Add(obj);
            }
        }
        public ObjectPooler(Func<PoolableObject> factoryMethod, Action<PoolableObject> turnOnCallback, Action<PoolableObject> turnOffCallback, int initialStock = 0, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = new List<PoolableObject>();

            for (var i = 0; i < initialStock; i++)
            {
                var obj = _factoryMethod();
                _turnOffCallback(obj);
                _currentStock.Add(obj);
            }
        }
        public ObjectPooler(Func<PoolableObject> factoryMethod, Action<PoolableObject> turnOnCallback, Action<PoolableObject> turnOffCallback, List<PoolableObject> initialStock, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = initialStock;
        }
        
        public PoolableObject GetObject()
        {
            var result = default(PoolableObject);
            if (_currentStock.Count > 0)
            {
                result = _currentStock[0];
                _currentStock.RemoveAt(0);
            }
            else if (_isDynamic)
                result = _factoryMethod();
            _turnOnCallback(result);
            return result;
        }
        
        public void ReturnObject(PoolableObject o)
        {
            _turnOffCallback(o);
            _currentStock.Add(o);
        }
    }
}