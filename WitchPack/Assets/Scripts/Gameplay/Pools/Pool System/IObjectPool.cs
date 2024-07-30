using UnityEngine;

namespace Gameplay.Pools.Pool_System
{
    public interface IObjectPool
    {
        IPoolable GetPooledObject();
        void ReturnPooledObject(IPoolable obj);
        public bool CheckActiveInstance();
        IPoolable Poolable { get; }
    }

    public interface IPoolable
    {
        GameObject PoolableGameObject { get; }
    }
}