using Systems.Pool_System;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    public static class EnergyParticleManager 
    {
        
        public static void SpawnParticle(Vector3 position, int energyValue)
        {
            var energyParticle = PoolManager.GetPooledObject<EnergyParticle>();
            energyParticle.Init(position,energyValue,Random.Range(0,360));
        }

        public static void PullAllParticlesToTarget(Transform target)
        {
            var energyParticles = PoolManager.GetActiveInstances<EnergyParticle>();
            if (energyParticles == null)
            {
                Debug.LogError("energy particles is null");
                return;
            }
            foreach (var particle in energyParticles)
            {
                particle.SetTarget(target);
            }
        }
    }
}