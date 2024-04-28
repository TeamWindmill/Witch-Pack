using UnityEngine;

namespace Systems.ObjectPool
{
    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
    {
        [SerializeField] private PoolableObject[] _poolableObjects;
    }
}