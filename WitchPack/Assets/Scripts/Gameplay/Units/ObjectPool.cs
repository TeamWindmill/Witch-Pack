using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] List<T> pooledObjects = new List<T>();
    public T PrefabToPool;
    public int NumToPool;
    public List<T> PooledObjects { get => pooledObjects; set => pooledObjects = value; }


    private void OnDestroy()
    {
        pooledObjects.Clear();
    }
    public void Awake()
    {
        for (int i = 0; i < NumToPool; i++)
        {
            T newPooledObject = Instantiate(PrefabToPool, transform);
            pooledObjects.Add(newPooledObject);
            newPooledObject.gameObject.SetActive(false);
        }
    }

    public T GetPooledObject()
    {
        foreach (var item in pooledObjects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        T newPooledObject = Instantiate(PrefabToPool, transform);
        pooledObjects.Add(newPooledObject);
        newPooledObject.gameObject.SetActive(false);
        return newPooledObject;

    }

    public bool CheckActiveIstance()
    {
        foreach (var item in pooledObjects)
        {
            if (item.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
