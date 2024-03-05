namespace Tools.Helpers
{
    public interface IInitialize<T>
    {
        public abstract void Init(T obj);
        public bool Initialized { get; }
    }

    public interface IInitialize<T1,T2>
    {
        public abstract void Init(T1 obj1,T2 obj2);
        public bool Initialized { get; }
    }
    public interface IInitialize<T1,T2,T3>
    {
        public abstract void Init(T1 obj1,T2 obj2,T3 obj3);
        public bool Initialized { get; }
    }
}