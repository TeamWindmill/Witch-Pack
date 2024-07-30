using Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Misc;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
namespace Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Items
{
    public abstract class EquipableItem : Item
    {
        [BoxGroup(STATS_BOX_GROUP)]
        public float Durability;

        [VerticalGroup(LEFT_VERTICAL_GROUP + "/Modifiers")]
        public StatList Modifiers;
    }
}
#endif
