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

    public T GetPooledObject(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
    {
        var item = GetPooledObject();
        if(parent != null) item.transform.parent = parent;
        item.transform.localPosition = position;
        item.transform.rotation = rotation;
        item.transform.localScale = scale;
        return item;
    }

    public T GetPooledObject()
    {
        foreach (var item in pooledObjects)
        {
            if (ReferenceEquals(item, null)) continue; 
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
    public void ReturnPooledObject(T obj)
    {
        obj.transform.parent = transform;
        obj.transform.position = Vector3.one;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.gameObject.SetActive(false);
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
