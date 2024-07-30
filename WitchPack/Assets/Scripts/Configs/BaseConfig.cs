using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    public abstract class BaseConfig : ScriptableObject
    {
        [BoxGroup("Unit")][HorizontalGroup("Unit/Split")]
        [VerticalGroup("Unit/Split/Left")]
        public string Name;
    }
}