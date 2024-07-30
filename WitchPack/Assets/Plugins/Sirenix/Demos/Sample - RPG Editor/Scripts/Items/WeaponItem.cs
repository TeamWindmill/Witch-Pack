using Sirenix.OdinInspector;

#if UNITY_EDITOR
namespace Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Items
{
    public class WeaponItem : EquipableItem
    {
        [BoxGroup(STATS_BOX_GROUP)]
        public float BaseAttackDamage;

        [BoxGroup(STATS_BOX_GROUP)]
        public float BaseAttackSpeed;

        [BoxGroup(STATS_BOX_GROUP)]
        public float BaseCritChance;

        [BoxGroup(STATS_BOX_GROUP)]
        public float CritChance;

        public override ItemTypes[] SupportedItemTypes
        {
            get
            {
                return new ItemTypes[]
                {
                    ItemTypes.MainHand,
                    ItemTypes.OffHand
                };
            }
        }
    }
}
#endif
