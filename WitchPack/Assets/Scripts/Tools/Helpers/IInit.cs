using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInit<T>
{
    public abstract void Init(T obj);
}

public interface IInit<T1,T2>
{
    public abstract void Init(T1 obj1,T2 obj2);
}
