using UnityEngine;

namespace Gameplay.Units.Visual
{
    public class ShamanEffectHandler : UnitEffectHandler
    {
        [SerializeField] protected Transform _castHands;

        private readonly Vector3 _femaleCastHandsPos = new (0.067f, 0.298f, 0);
        private readonly Vector3 _maleCastHandsPos = new (0, 0.3f, 0);

        public override void Init(BaseUnitConfig config)
        {
            base.Init(config);
            _castHands.localPosition = (config as ShamanConfig).IsMale ? _maleCastHandsPos : _femaleCastHandsPos;
        }
    }
}