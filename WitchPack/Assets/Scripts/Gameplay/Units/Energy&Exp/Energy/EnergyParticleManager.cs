using Systems.Pool_System;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    public class EnergyParticleManager 
    {
        
        public static void SpawnParticle(Vector3 position, int energyValue)
        {
            var EnergyParticle = PoolManager.GetPooledObject<EnergyParticle>();
            EnergyParticle.Init(position,energyValue,Random.Range(0,360));
        }
    }
}