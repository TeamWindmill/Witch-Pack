using System;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public UnitStats Stats => _stats;
    public Shaman Shaman => _shaman;
    public Dictionary<StatType, int> CurrentStatPSEffects => currentStatPSEffects;

    
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private UnitStats _stats;
    private Shaman _shaman;
    private bool _isActive;

    private Dictionary<StatType, int> currentStatPSEffects;


    public void Show(Shaman shaman)
    {
        currentStatPSEffects = new Dictionary<StatType, int>();
        _stats = shaman.Stats;
        spriteRenderer.sprite = shaman.ShamanConfig.UnitSprite;
        _shaman = shaman;
        rangeTransform.localScale = new Vector3(shaman.Stats.BonusRange * 2, shaman.Stats.BonusRange * 2, 0);
        gameObject.SetActive(true);
        _isActive = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);   
        _isActive = false;
    }

    public void SetPSStatValue(StatType statType, int value)
    {
        if (currentStatPSEffects.ContainsKey(statType))
        {
            currentStatPSEffects[statType] += value;
        }
        else
        {
            currentStatPSEffects.Add(statType,value);
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            Vector3 newPos = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = _shaman.transform.position.z;
            transform.position = newPos;
            lineRenderer.positionCount = 2; 
            lineRenderer.SetPositions(new Vector3[] { _shaman.transform.position, transform.position });
        }
    }

    
}
