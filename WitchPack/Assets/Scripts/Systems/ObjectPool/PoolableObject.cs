using UnityEngine;

namespace Systems.ObjectPool
{
    public abstract class PoolableObject<T> : MonoBehaviour
    {
        [SerializeField] protected int _initialStock;
        [SerializeField] protected bool _isDynamic;
        public abstract T FactoryMethod();
        public abstract void TurnOnCallback(T obj);
        public abstract void TurnOffCallback(T obj);
        public int InitialStock => _initialStock;
        public bool IsDynamic => _isDynamic;
    }
}