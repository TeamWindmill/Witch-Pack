using UnityEngine;

namespace Tools.Helpers
{
    public abstract class InitializedMono<T> : MonoBehaviour
    {
        public virtual void Init(T obj) => Initialized = true;
        public bool Initialized { get; private set; }
    }
    public abstract class InitializedMono<T1,T2> : MonoBehaviour
    {
        public virtual void Init(T1 obj1,T2 obj2) => Initialized = true;
        public bool Initialized { get; private set; }
    }
    public abstract class InitializedMono<T1,T2,T3> : MonoBehaviour
    {
        public virtual void Init(T1 obj1,T2 obj2,T3 obj3) => Initialized = true;
        public bool Initialized { get; private set; }
    }
    public abstract class InitializedMono<T1,T2,T3,T4> : MonoBehaviour
    {
        public virtual void Init(T1 obj1,T2 obj2,T3 obj3,T4 obj4) => Initialized = true;
        public bool Initialized { get; private set; }
    }
}