using UnityEngine;

namespace External_Assets.DamageNumbersPro.Scripts.Settings
{
    [System.Serializable]
    public struct PushSettings
    {
        public PushSettings(float customDefault)
        {
            radius = 4f;
            pushOffset = 0.8f;
        }

        [Header("Main:")]
        public float radius;
        public float pushOffset;
    }
}
