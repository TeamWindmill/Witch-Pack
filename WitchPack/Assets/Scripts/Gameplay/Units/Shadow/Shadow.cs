using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public UnitStats Stats => _stats;
    public StatSheet BaseStats => _baseStats;
    public Shaman Shaman => _shaman;

    
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private UnitStats _stats;
    private StatSheet _baseStats;
    private Shaman _shaman;
    private bool _isActive;

    public void Show(Shaman shaman)
    {
        _baseStats = shaman.BaseStats;
        _stats = new UnitStats(_baseStats);
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
