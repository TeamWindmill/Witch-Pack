using System;
using Gameplay.Units.Stats;

namespace Gameplay.Units.Damage_System
{
    public class Affector 
    {
        private IDamagable owner;

        public Action<Effectable, Affector, StatusEffect> OnAffect;
        public IDamagable Owner { get => owner; }
        public Affector(IDamagable owner)
        {
            this.owner = owner;
        }

    }
}
