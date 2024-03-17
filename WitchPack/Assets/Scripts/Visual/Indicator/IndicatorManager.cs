using UnityEngine;
using System;

public class IndicatorManager : MonoBehaviour
{ 
    public Indicator CreateIndicator(Indicatable target)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.transform.SetParent(LevelManager.Instance.GameUi.transform);
        indicator.gameObject.SetActive(true);
        indicator.InitIndicator(target, target.ArtWork, target.Lifetime, target.Clickable, target.OnClickAction, target.ShouldIndicatorPulse, target.IndicatorPointerSpriteType);
        return indicator;
    }


}
