using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pool_System
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private IndicatorPool indicatorPool;
        
        private static Dictionary<Type, IObjectPool> _pools;

        private void Awake()
        {
            _pools = new();
            var pools = GetComponentsInChildren<IObjectPool>();
            _pools.Add(indicatorPool.Poolable.GetType(), indicatorPool);
            foreach (var pool in pools)
            {
                _pools.Add(pool.Poolable.GetType(), pool);
            }
        }
        public static T GetPooledObject<T>() where T : class, IPoolable
        {
            return _pools[typeof(T)].GetPooledObject() as T;
        }
        public static bool CheckActiveInstance<T>() where T : class, IPoolable
        {
            return _pools[typeof(T)].CheckActiveInstance();
        }
    }
}
