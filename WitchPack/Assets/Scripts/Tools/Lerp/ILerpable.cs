using System;

namespace Tools.Lerp
{

    [Serializable]
    public struct LerpConfig<T>
    {
        public float TransitionTime;
        public T StartValue;
        public T EndValue;
    }
}