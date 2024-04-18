using UnityEngine;

namespace Systems.ObjectPool
{
    public abstract class PoolableObject : MonoBehaviour
    {
        [SerializeField] protected int _initialStock;
        [SerializeField] protected bool _isDynamic;
        public abstract PoolableObject FactoryMethod();
        public abstract void TurnOnCallback(PoolableObject obj);
        public abstract void TurnOffCallback(PoolableObject obj);
        public int InitialStock => _initialStock;
        public bool IsDynamic => _isDynamic;
    }
}