using System;
using Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Items;

#if UNITY_EDITOR
namespace Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Misc
{
    [Serializable]
    public struct ItemSlot
    {
        public int ItemCount;
        public Item Item;
    }
}
#endif
