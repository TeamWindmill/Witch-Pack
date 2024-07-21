using Tools.Helpers;
using UnityEngine;

public class EnviromentHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _windEffects;
    [SerializeField] private ParticleSystem[] _butteflyEffects;
    [SerializeField] private Animator _waterfallAnimator;

    public ParticleSystem[] WindEffects => _windEffects;

    public ParticleSystem[] ButteflyEffects => _butteflyEffects;

    public Animator WaterfallAnimator => _waterfallAnimator;

    public void SetSlowMotion()
    {
        
    }

    public void EndSlowMotion()
    {
        
    }
}