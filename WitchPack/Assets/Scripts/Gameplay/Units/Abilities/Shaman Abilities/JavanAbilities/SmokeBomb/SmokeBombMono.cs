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

    private Dictionary<Shaman,StatusEffect[]> _affectedShamans = new Dictionary<Shaman,StatusEffect[]>();
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
        transform.localScale = new Vector3(ability.SmokeBombConfig.Range, ability.SmokeBombConfig.Range, 0);
        Invoke(nameof(EndBomb),ability.SmokeBombConfig.Duration);
        _targeter.OnTargetAdded += OnTargetEntered;
        _targeter.OnTargetLost += OnTargetExited;
    }
    protected virtual void OnTargetEntered(GroundCollider collider)
    {
        var unit = collider.Unit;
        if (unit is not Shaman shaman) return;
        if (_affectedShamans.ContainsKey(shaman)) return;

        StatusEffect[] statusEffects = new StatusEffect[_ability.SmokeBombConfig.StatusEffects.Count];
        for (int i = 0; i < _ability.SmokeBombConfig.StatusEffects.Count; i++)
        {
            statusEffects[i] = shaman.Effectable.AddEffect(_ability.SmokeBombConfig.StatusEffects[i],_owner.Affector);
        }
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
        rangeExit.stopped += OnEnd;
    }

    protected virtual void OnEnd(PlayableDirector director)
    {
        cloudsExit.gameObject.SetActive(false);
        rangeExit.gameObject.SetActive(false);
        gameObject.SetActive(false);
        director.stopped -= OnEnd;
        _targeter.OnTargetAdded -= OnTargetEntered;
        _targeter.OnTargetLost -= OnTargetExited;
    }
}
