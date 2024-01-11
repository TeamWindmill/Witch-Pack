using UnityEngine;

public class IndicatorManager : MonoBehaviour
{

    public Indicator CreateIndicator(Transform target, Sprite artwork, float time = 0)
    {
        Indicator indicator = LevelManager.Instance.PoolManager.InidcatorPool.GetPooledObject();
        indicator.InitIndicator(target, artwork, time);
        indicator.gameObject.SetActive(true);
        return indicator;
        //subscribe shaman on non visible to remove
    }


}
