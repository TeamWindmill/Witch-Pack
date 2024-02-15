using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SmokeBomb : MonoBehaviour
{
    public event Action OnAbilityEnd;
    
    [SerializeField] private ShamanTargeter shamanTargeter;
    [SerializeField] private EnemyTargeter enemyTargeter;
    
    [Header("Timelines")]
    [SerializeField] private PlayableDirector rangeEnter;
    [SerializeField] private PlayableDirector rangeExit;
    [SerializeField] private PlayableDirector cloudsEnter;
    [SerializeField] private PlayableDirector cloudsIdle;
    [SerializeField] private PlayableDirector cloudsExit;

    private Dictionary<Shaman,StatusEffect> _affectedShamans = new Dictionary<Shaman,StatusEffect>();
    private SmokeBombSO _config;
    private BaseUnit _owner;

    private void Awake()
    {
        rangeEnter.gameObject.SetActive(false);
        rangeExit.gameObject.SetActive(false);
        cloudsEnter.gameObject.SetActive(false);
        cloudsIdle.gameObject.SetActive(false);
        cloudsExit.gameObject.SetActive(false);
    }

    public void SpawnBomb(SmokeBombSO config, BaseUnit owner)
    {
        _config = config;
        _owner = owner;
        rangeEnter.gameObject.SetActive(true);
        cloudsEnter.gameObject.SetActive(true);
        cloudsEnter.stopped += CloudsIdleAnim;
        //transform.localScale = new Vector3(config.Range, config.Range, 0);
        Invoke(nameof(EndBomb),config.Duration);
        //shamanTargeter.OnTargetAdded += OnTargetEntered;
        //shamanTargeter.OnTargetLost += OnTargetExited;
    }
    private void OnTargetEntered(GroundCollider collider)
    {
        var unit = collider.Unit;
        if (unit is not Shaman shaman) return;
        if (_affectedShamans.ContainsKey(shaman)) return;
        
        foreach (var statusEffect in _config.StatusEffects)
        {
            var effect = shaman.Effectable.AddEffect(statusEffect,_owner.Affector);
            _affectedShamans.Add(shaman,effect);
        }
    }
    private void OnTargetExited(GroundCollider collider)
    {
        var shaman = collider.GetComponentInParent<Shaman>();
        RemoveShamanEffect(shaman);
    }
    
    private void CloudsIdleAnim(PlayableDirector clip)
    {
        clip.gameObject.SetActive(false);
        cloudsIdle.gameObject.SetActive(true);
        clip.stopped -= CloudsIdleAnim;
    }
    

    private void EndBomb()
    {
        cloudsIdle.gameObject.SetActive(false);
        rangeEnter.gameObject.SetActive(false);
        cloudsExit.gameObject.SetActive(true);
        rangeExit.gameObject.SetActive(true);
        OnAbilityEnd?.Invoke();
        rangeExit.stopped += OnEnd;
    }

    private void OnEnd(PlayableDirector director)
    {
        cloudsExit.gameObject.SetActive(false);
        rangeExit.gameObject.SetActive(false);
        gameObject.SetActive(false);
        director.stopped -= OnEnd;
    }

    private void RemoveShamanEffect(Shaman shaman)
    {
        if (_affectedShamans.TryGetValue(shaman,out var effect))
        {
            effect.RemoveEffectFromShaman();
            _affectedShamans.Remove(shaman);
        } 
    }
}
