using Managers;
using Sirenix.OdinInspector;
using Sound;
using GameTime;
using Tools.Helpers;
using UnityEngine;

namespace Tools.SlowMotion
{
    public class SlowMotionManager : MonoSingleton<SlowMotionManager>
    {
        [SerializeField] private AudioFilters _audioFilters;
        [SerializeField] private PostProcessSlowMotionEffect _postProcessFilters;
        [SerializeField] private WindEffectHandler _windEffectHandler;
        [SerializeField] private ButterflyEffectHandler _butterflyEffectHandler;
        [SerializeField] private WaterfallEffectHandler _waterfallEffectHandler;

        [TabGroup("Time"), SerializeField] private AnimationCurve _startSlowTimeCurve;
        [TabGroup("Time"), SerializeField] private AnimationCurve _endSlowTimeCurve;
        [TabGroup("Time"), SerializeField] private float _slowTimeTransitionTime;
        [TabGroup("Time"), SerializeField] private float _slowTime;
        private float _previousTimeRate;
    
        public bool IsActive { get; private set; }

        private void Start()
        {
            _audioFilters.Init(BgMusicManager.Instance.AudioSource,
                BgMusicManager.Instance.AudioReverbFilter, BgMusicManager.Instance.AudioLowPassFilter);
            _postProcessFilters.Init(GameManager.CameraHandler.PostProcessVolume);
        
            if(LevelManager.Instance.CurrentLevel.EnviromentHandler == null) return;
        
            _windEffectHandler.Init(LevelManager.Instance.CurrentLevel.EnviromentHandler.WindEffects);
            _butterflyEffectHandler.Init(LevelManager.Instance.CurrentLevel.EnviromentHandler.ButteflyEffects);
            _waterfallEffectHandler.Init(LevelManager.Instance.CurrentLevel.EnviromentHandler.WaterfallAnimator);
        }

        public void StartSlowMotionEffects()
        {
            _previousTimeRate = GAME_TIME.TimeRate;
            if(_previousTimeRate == 0) return;
            GAME_TIME.SetTimeStep(_slowTime, _slowTimeTransitionTime, _startSlowTimeCurve);
            _audioFilters.StartTransition();
            _postProcessFilters.StartTransition();
        
            if(LevelManager.Instance.CurrentLevel.EnviromentHandler == null) return;

            _windEffectHandler.StartTransition();
            _butterflyEffectHandler.StartTransition();
            _waterfallEffectHandler.StartTransition();
            IsActive = true;
        }

        public void EndSlowMotionEffects()
        {
            if(_previousTimeRate == 0) return;
            if(GAME_TIME.TimeRate == 0) return;
            GAME_TIME.SetTimeStep(_previousTimeRate, _slowTimeTransitionTime, _endSlowTimeCurve);
            _audioFilters.EndTransition();
            _postProcessFilters.EndTransition();
        
            if(LevelManager.Instance.CurrentLevel.EnviromentHandler == null) return;

            _windEffectHandler.EndTransition();
            _butterflyEffectHandler.EndTransition();
            _waterfallEffectHandler.EndTransition();
            IsActive = false;
        }
    }
}