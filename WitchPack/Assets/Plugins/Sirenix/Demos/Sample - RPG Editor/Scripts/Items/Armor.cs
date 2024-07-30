using Sirenix.OdinInspector;

#if UNITY_EDITOR
namespace Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Items
{
    public class Armor : EquipableItem
    {
        [BoxGroup(STATS_BOX_GROUP)]
        public float BaseArmor;

        [BoxGroup(STATS_BOX_GROUP)]
        public float MovementSpeed;

        public override ItemTypes[] SupportedItemTypes
        {
            get
            {
                return new ItemTypes[] 
                {
                    ItemTypes.Body,
                    ItemTypes.Head,
                    ItemTypes.Boots,
                    ItemTypes.OffHand
                };
            }
        }
    }
}
#endif
