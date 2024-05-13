using DG.Tweening;
using UnityEngine;

namespace Tools.Lerp
{
    public class DoTweenManager : MonoBehaviour
    {
        private void Awake()
        {
            DOTween.defaultAutoPlay = AutoPlay.AutoPlayTweeners;
            DOTween.defaultLoopType = LoopType.Yoyo;
        }
    }
}