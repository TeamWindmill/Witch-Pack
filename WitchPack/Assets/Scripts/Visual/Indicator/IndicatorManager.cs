using UnityEngine;

public class IndicatorManager : MonoBehaviour
{ 
    public Indicator CreateIndicator(Transform target, Sprite artwork, float time)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.transform.SetParent(LevelManager.Instance.GameUi.transform);
        indicator.InitIndicator(target, artwork, time);
        indicator.gameObject.SetActive(true);
        return indicator;
    }


}
