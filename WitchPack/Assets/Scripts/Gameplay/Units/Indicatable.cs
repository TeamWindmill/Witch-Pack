using UnityEngine;

public class Indicatable : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    private Indicator currentIndicator;
    private Sprite artWork;
    private float lifetime;

    public Renderer Rend { get => rend; }

    public void Init(Sprite art, float lifetime = 0)
    {
        this.lifetime = lifetime;
        artWork = art;
    }

    public bool IsVisible()
    {
        if (!rend.isVisible)
        {
            return false;
        }
        return true;
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
        currentIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(this, artWork, lifetime);
    }


}
