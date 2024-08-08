using UnityEngine;

namespace Systems.Pool_System
{
    public interface IPoolable
    {
        GameObject PoolableGameObject { get; }
    }
    
    public interface IObjectPool
    {
        IPoolable GetPooledObject();
        void ReturnPooledObject(IPoolable obj);
        public bool CheckActiveInstance();
        IPoolable Poolable { get; }
        IPoolable[] GetActiveInstances();
    }
}