using System;

namespace Systems.ObjectPool
{
    public interface IPoolable<T>
    {
        int InitialStock { get;}
        bool IsDynamic { get;}
        T FactoryMethod();
        void TurnOnCallback(T obj);
        void TurnOffCallback(T obj);
    }
}