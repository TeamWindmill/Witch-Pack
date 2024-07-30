using Gameplay.Units.Abilities.AbilitiesMono.General_Abilities;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Rooting_Vines
{
    public class RootingVinesMono : AoeMono
    {
    
        protected virtual void OnRoot(Enemy.Enemy enemy){}

        protected override void OnEnemyEnter(Enemy.Enemy enemy)
        {
            OnRoot(enemy);
            enemy.Damageable.GetHit(_owner.DamageDealer, Ability);
        }
    }
}
