using UnityEngine;
using System;

public class IndicatorManager : MonoBehaviour
{ 
    public Indicator CreateIndicator(Indicatable target, Sprite artwork, float time, bool clickable, Action onClick)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.transform.SetParent(LevelManager.Instance.GameUi.transform);
        indicator.gameObject.SetActive(true);
        indicator.InitIndicator(target, artwork, time, clickable, onClick);
        return indicator;
    }


}
