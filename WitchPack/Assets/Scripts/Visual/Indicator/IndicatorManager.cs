using UnityEngine;

public class IndicatorManager : MonoBehaviour
{ 
    public Indicator CreateIndicator(Indicatable target, Sprite artwork, float time)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.transform.SetParent(LevelManager.Instance.GameUi.transform);
        indicator.gameObject.SetActive(true);
        indicator.InitIndicator(target, artwork, time);
        return indicator;
    }


}
