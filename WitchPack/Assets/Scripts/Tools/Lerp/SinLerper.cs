using Unity.Mathematics;
using UnityEngine;

namespace Tools.Lerp
{
    public class SinLerper
    {
        public Vector3 Value;
        private float _lerpTimer;
        private Vector3 StartPos;
        private Vector3 EndPos;

        public SinLerper(Vector3 center, Vector3 movement, float startValue)
        {
            StartPos = center - movement;
            EndPos = center + movement;
            _lerpTimer = startValue;
        }

        public void Update()
        {
            _lerpTimer += Time.deltaTime;
            var sinTimer = (math.sin(_lerpTimer) + 1)/2;
            Value = Vector3.Lerp(StartPos, EndPos, sinTimer);
        }
    }
}