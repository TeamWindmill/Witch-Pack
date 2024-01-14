using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Image artwork;
    [SerializeField] private Image circle;
    [SerializeField] private Transform pointer;
    [SerializeField] private LayerMask layers;

    private float time;
    private float counter;
    Indicatable target;
    Vector3 midScreen;

    private Vector2 screenSize = new Vector2(Screen.width, Screen.height);

    public void InitIndicator(Indicatable target, Sprite artwork, float time)
    {
        if (target.IsVisible())
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 targetSP = GameManager.Instance.CameraHandler.MainCamera.WorldToScreenPoint(target.transform.position);
        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        counter = 0f;
        circle.fillAmount = 1;
        float x;
        float y;
        if (targetSP.x > 0)
        {
            x = screenSize.x / 2;
        }
        else
        {
            x = -screenSize.x / 2;
        }
        if (targetSP.y > 0)
        {
            y = screenSize.y / 2;
        }
        else
        {
            y = -screenSize.y / 2;
        }
        Vector2 dir = targetSP - GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(screenSize / 2);
        transform.localPosition = new Vector2(x, y) * dir.normalized;

    }


    private void OnDrawGizmosSelected()
    {
        Vector3 targetSP = GameManager.Instance.CameraHandler.MainCamera.WorldToScreenPoint(target.transform.position);

        float x;
        float y;
        if (targetSP.x > 0)
        {
            x = screenSize.x / 2;
        }
        else
        {
            x = -screenSize.x / 2;
        }
        if (targetSP.y > 0)
        {
            y = screenSize.y / 2;
        }
        else
        {
            y = -screenSize.y / 2;
        }
       // Vector2 to = targetSP - GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(screenSize / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(screenSize / 2), targetSP);
    }

}


