using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LineRenderer lineRenderer;

    private Transform _shamanTransform;
    private Shaman _shaman;
    private bool _isActive;

    public void Show(Shaman shaman)
    {
        spriteRenderer.sprite = shaman.ShamanConfig.UnitSprite;
        _shamanTransform = shaman.transform;
        _shaman = shaman;
        rangeTransform.localScale = new Vector3(shaman.Stats.BonusRange, shaman.Stats.BonusRange, 0);
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
            newPos.z = _shamanTransform.position.z;
            transform.position = newPos;
            lineRenderer.positionCount = 2; 
            lineRenderer.SetPositions(new [] { _shamanTransform.position, transform.position });
        }
    }
}
