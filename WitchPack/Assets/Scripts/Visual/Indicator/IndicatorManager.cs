using UnityEngine;
using System.Collections.Generic;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField]private List<Indicator> _activeIndicators;
    public List<Indicator> ActiveIndicators => _activeIndicators;
    public Indicator CreateIndicator(Indicatable target)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.transform.SetParent(LevelManager.Instance.GameUi.transform);
        indicator.gameObject.SetActive(true);
        indicator.InitIndicator(target, target.ArtWork, target.Lifetime, target.Clickable, target.OnClickAction, target.ShouldIndicatorPulse, target.IndicatorPointerSpriteType);
        _activeIndicators.Add(indicator);
        return indicator;
    }
    public void RemoveActiveIndicator(Indicator indicator)
    { 
        _activeIndicators.Remove(indicator);
    }

}
