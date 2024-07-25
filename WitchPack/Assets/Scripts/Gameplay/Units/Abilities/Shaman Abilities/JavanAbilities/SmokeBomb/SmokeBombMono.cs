using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SmokeBombMono : MonoBehaviour
{
    public event Action OnAbilityEnd;
    
    [SerializeField] private GroundColliderTargeter _targeter;
    
    [Header("Timelines")]
    [SerializeField] private PlayableDirector rangeEnter;
    [SerializeField] private PlayableDirector rangeExit;
    [SerializeField] private PlayableDirector cloudsEnter;
    [SerializeField] private PlayableDirector cloudsIdle;
    [SerializeField] private PlayableDirector cloudsExit;

    private Dictionary<Shaman,StatusEffect[]> _affectedShamans = new ();
    protected SmokeBomb _ability;
    protected BaseUnit _owner;

    private void Awake()
    {
        rangeEnter.gameObject.SetActive(false);
        rangeExit.gameObject.SetActive(false);
        cloudsEnter.gameObject.SetActive(false);
        cloudsIdle.gameObject.SetActive(false);
        cloudsExit.gameObject.SetActive(false);
    }

    public virtual void SpawnBomb(SmokeBomb ability, BaseUnit owner)
    {
        _ability = ability;
        _owner = owner;
        rangeEnter.gameObject.SetActive(true);
        cloudsEnter.gameObject.SetActive(true);
        cloudsEnter.stopped += CloudsIdleAnim;
        transform.localScale = new Vector3(ability.GetAbilityStatValue(AbilityStatType.Size), ability.GetAbilityStatValue(AbilityStatType.Size), 0);
        TimerManager.AddTimer(ability.GetAbilityStatValue(AbilityStatType.Duration), EndBomb,true);
        _targeter.OnTargetAdded += OnTargetEntered;
        _targeter.OnTargetLost += OnTargetExited;
        GAME_TIME.OnTimeRateChange += SetTime;
    }
    protected virtual void OnTargetEntered(GroundCollider collider)
    {
        var unit = collider.Unit;
        if (unit is not Shaman shaman) return;
        if (_affectedShamans.ContainsKey(shaman)) return;

        StatusEffect[] statusEffects = shaman.Effectable.AddEffects(_ability.StatusEffects,_owner.Affector).ToArray();
        _affectedShamans.Add(shaman,statusEffects);
    }
    private void OnTargetExited(GroundCollider collider)
    {
        if (collider.Unit is Shaman shaman)
        {
            if (_affectedShamans.TryGetValue(shaman,out var statusEffects))
            {
                foreach (var effect in statusEffects)
                {
                    effect.Remove();
                }
                _affectedShamans.Remove(shaman);
            } 
        }
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
    }

    protected virtual void OnEnd(PlayableDirector director)
    {
        cloudsExit.gameObject.SetActive(false);
        rangeExit.gameObject.SetActive(false);
        gameObject.SetActive(false);
        director.stopped -= OnEnd;
        _targeter.OnTargetAdded -= OnTargetEntered;
        _targeter.OnTargetLost -= OnTargetExited;
        rangeExit.stopped += OnEnd;
        GAME_TIME.OnTimeRateChange -= SetTime;
    }

    private void SetTime(float newTime)
    {
        if (newTime == 0)
        {
            rangeEnter.Pause();
            rangeExit.Pause();
            cloudsEnter.Pause();
            cloudsIdle.Pause();
            cloudsExit.Pause();
        }
        else
        {
            rangeEnter.Resume();
            rangeExit.Resume();
            cloudsEnter.Resume();
            cloudsIdle.Resume();
            cloudsExit.Resume();
        }
        
    }
}
