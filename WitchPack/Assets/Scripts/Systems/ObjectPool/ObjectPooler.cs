using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems.ObjectPool
{
    public class ObjectPooler<T>
    {
        private readonly List<T> _currentStock;
        private readonly bool _isDynamic;
        private readonly Func<T> _factoryMethod;
        private readonly Action<T> _turnOnCallback;
        private readonly Action<T> _turnOffCallback;

        public ObjectPooler(PoolableObject<T> poolable)
        {
            _factoryMethod = poolable.FactoryMethod;
            _isDynamic = poolable.IsDynamic;

            _turnOffCallback = poolable.TurnOffCallback;
            _turnOnCallback = poolable.TurnOnCallback;

            _currentStock = new List<T>();

            for (var i = 0; i < poolable.InitialStock; i++)
            {
                var obj = _factoryMethod();
                _turnOffCallback(obj);
                _currentStock.Add(obj);
            }
        }
        public ObjectPooler(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = new List<T>();

            for (var i = 0; i < initialStock; i++)
            {
                var obj = _factoryMethod();
                _turnOffCallback(obj);
                _currentStock.Add(obj);
            }
        }
        public ObjectPooler(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, List<T> initialStock, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = initialStock;
        }
        
        public T GetObject()
        {
            var result = default(T);
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
        
        public void ReturnObject(T o)
        {
            _turnOffCallback(o);
            _currentStock.Add(o);
        }
    }
}