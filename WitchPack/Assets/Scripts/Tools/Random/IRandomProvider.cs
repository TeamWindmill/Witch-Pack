namespace Tools.Random
{
    public interface IRandomProvider<T>
    {
        T Provide();
    }
}