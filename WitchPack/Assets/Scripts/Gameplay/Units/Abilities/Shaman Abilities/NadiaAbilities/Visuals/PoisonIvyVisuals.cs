using GameTime;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Visuals
{
    public class PoisonIvyVisuals : MonoBehaviour
    {
        private float elapsedTime;
        private float duration;

        private ParticleSystem poisonIvyParticle;
    


        // Update is called once per frame
        void Update()
        {
            elapsedTime += GAME_TIME.GameDeltaTime;
            if(elapsedTime >= duration)
            {
                StopPoisonParticle();
            }
        }

        public void PlayPoisonParticle(float duration)
        {
            elapsedTime = 0;
            this.duration = duration;
            gameObject.SetActive(true);
        }

        public void StopPoisonParticle()
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
        }


    }
}
