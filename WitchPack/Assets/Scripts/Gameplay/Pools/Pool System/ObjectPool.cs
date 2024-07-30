using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Pools.Pool_System
{
    public class ObjectPool<T> : MonoBehaviour, IObjectPool where T : MonoBehaviour, IPoolable
    {
        public T PrefabToPool;
        public int NumToPool;
        public List<T> PooledObjects { get; set; } = new List<T>();


        private void OnDestroy()
        {
            PooledObjects.Clear();
        }
        public void Awake()
        {
        
            for (int i = 0; i < NumToPool; i++)
            {
                T newPooledObject = Instantiate(PrefabToPool, transform);
                PooledObjects.Add(newPooledObject);
                newPooledObject.PoolableGameObject.SetActive(false);
            }
        }

        public IPoolable GetPooledObject()
        {
            foreach (var item in PooledObjects)
            {
                if (ReferenceEquals(item, null)) continue; 
                if (!item.PoolableGameObject.gameObject.activeInHierarchy)
                {
                    return item;
                }
            }

            T newPooledObject = Instantiate(PrefabToPool, transform);
            PooledObjects.Add(newPooledObject);
            newPooledObject.PoolableGameObject.SetActive(false);
            return newPooledObject;
        }
        public void ReturnPooledObject(IPoolable obj)
        {
            obj.PoolableGameObject.transform.parent = transform;
            obj.PoolableGameObject.transform.position = Vector3.one;
            obj.PoolableGameObject.transform.rotation = Quaternion.identity;
            obj.PoolableGameObject.transform.localScale = Vector3.one;
            obj.PoolableGameObject.SetActive(false);
        }

        public bool CheckActiveInstance()
        {
            foreach (var item in PooledObjects)
            {
                if (item.PoolableGameObject.activeInHierarchy)
                {
                    return true;
                }
            }
            return false;
        }

        public IPoolable Poolable => PrefabToPool;
    }
}
