using System.Collections.Generic;
using Gameplay.Units.Stats;
using Managers;
using UnityEngine;

namespace Gameplay.Units.Shadow
{
    public class Shadow : MonoBehaviour
    {
        public UnitStats Stats => _stats;
        public Shaman.Shaman Shaman => _shaman;
        public Dictionary<StatType, float> CurrentStatPSEffects => currentStatPSEffects;
        public bool IsActive => _isActive;

        [SerializeField] private Transform rangeTransform;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private UnitStats _stats;
        private Shaman.Shaman _shaman;
        private bool _isActive;

        private Dictionary<StatType, float> currentStatPSEffects;

        public void Show(Shaman.Shaman shaman)
        {
            currentStatPSEffects = new();
            _stats = shaman.Stats;
            spriteRenderer.sprite = shaman.ShamanConfig.UnitSprite;
            _shaman = shaman;
            rangeTransform.localScale = new Vector3(shaman.Stats[StatType.BaseRange].Value, shaman.Stats[StatType.BaseRange].Value, 0);
            gameObject.SetActive(true);
            _isActive = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);   
            _isActive = false;
        }

        public void SetPSStatValue(StatType statType, float value)
        {
            if (currentStatPSEffects.ContainsKey(statType))
            {
                currentStatPSEffects[statType] += value;
            }
            else
            {
                currentStatPSEffects.Add(statType,value);
            }
        
            if (statType == StatType.BaseRange)
            {
                var range = _shaman.Stats[StatType.BaseRange].Value + currentStatPSEffects[StatType.BaseRange];
                ChangeRange(range);
            }
        }

        private void Update()
        {
            if (_isActive)
            {
                Vector3 newPos = GameManager.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                newPos.z = _shaman.transform.position.z;
                transform.position = newPos;
                lineRenderer.positionCount = 2; 
                lineRenderer.SetPositions(new Vector3[] { _shaman.transform.position, transform.position });
            }
        }

        private void ChangeRange(float range)
        {
            rangeTransform.localScale = new Vector3(range, range, 0);
        }

    
    }
}
