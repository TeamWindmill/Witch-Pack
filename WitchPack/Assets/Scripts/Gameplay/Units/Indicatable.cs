using UnityEngine;

public class Indicatable : MonoBehaviour
{
    private Indicator currentIndicator;
    private Sprite artWork;
    private float lifetime;
    public void Init(Sprite art, float lifetime = 0)
    {
        this.lifetime = lifetime;
        artWork = art;
    }


    /*private void OnBecameVisible()
    {
        if (!ReferenceEquals(currentIndicator, null))
        {
            currentIndicator?.gameObject.SetActive(false);
            currentIndicator = null;
        }
    }*/

   /* private void OnBecameInvisible()
    {
    }
*/
    [ContextMenu("Test Indicator")]
    public void TestIndicator()
    {
        currentIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(transform, artWork, lifetime);

    }
}
