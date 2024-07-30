using GameTime;
using UnityEngine;
using UnityEngine.Playables;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Visuals
{
    public class RootingVinesVisuals : MonoBehaviour
    {
        //[SerializeField] StatusEffectConfig root;
        [SerializeField] PlayableDirector exitAnimation;
        [SerializeField] private GameObject entryGameObject;
        [SerializeField] private GameObject exitGameObject;

        float elapsedTime;
        float exitTime;
        private bool _isInitialized;

        public void Init(float duration)
        {
            exitTime = duration - (float)exitAnimation.duration;
            elapsedTime = 0;
            SwitchGameObjects(true);
            _isInitialized = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isInitialized) return;
            elapsedTime += GAME_TIME.GameDeltaTime;
            if (elapsedTime >= exitTime)
            {
                SwitchGameObjects(false);
            }
        }

        private void OnDisable()
        {
            _isInitialized = false;
        }

        private void SwitchGameObjects(bool entryState)
        {
            entryGameObject.SetActive(entryState);
            exitGameObject.SetActive(!entryState);
        }
    }
}
