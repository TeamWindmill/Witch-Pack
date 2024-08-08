using System.Collections;
using DG.Tweening;
using Systems.Pool_System;
using UnityEngine;

public class EnergyParticle : MonoBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private ShamanTargeter shamanTargeter;
    [SerializeField] private CoreTargeter coreTargeter;
    [SerializeField] private float magnetSpeed;
    [SerializeField] private float dropDuration;
    [SerializeField] private float dropRadius;
    [SerializeField] private Ease dropAnimationEase;
    private int _energyValue;
    private bool _targetSet;

    private void Awake()
    {
        shamanTargeter.OnTargetAdded += (s) => PopEnergyParticle();
        coreTargeter.OnTargetAdded += (c) => PopEnergyParticle();
    }

    public void Init(Vector3 position, int energyValue, int randomAngle)
    {
        _targetSet = false;
        _energyValue = energyValue;
        transform.position = position;
        gameObject.SetActive(true);
        ParticleDropAnimation(position, randomAngle);
    }

    public void SetTarget(Transform target)
    {
        StartCoroutine(MoveToTarget(target));
        _targetSet = true;
    }

    private void ParticleDropAnimation(Vector3 position, int randomAngle)
    {
        var x = Mathf.Sin(randomAngle);
        var y = Mathf.Cos(randomAngle);
        transform.DOMove(position + new Vector3(x, y, 0) * dropRadius, dropDuration).SetEase(dropAnimationEase);
    }

    private void PopEnergyParticle()
    {
        PartyEnergyHandler.AddEnergy(_energyValue);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_targetSet) return;
        if (other.TryGetComponent<Shaman>(out var shaman))
        {
            SetTarget(shaman.transform);
        }
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        while(transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, magnetSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public GameObject PoolableGameObject => gameObject;
}