using System;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : UIElement
{
    [SerializeField] private Image artwork;
    [SerializeField] private Image circle;
    [SerializeField] private Button button;
    [SerializeField] private Transform pointer;

    private Action onClick;
    private float time;
    private float counter;
    private Indicatable target;

    private Vector3 midScreen = new Vector3(Screen.width / 2, Screen.height / 2);

    public void InitIndicator(Indicatable target, Sprite artwork, float time, bool clickable, Action onClick = null)
    {
        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        counter = 0f;
        circle.fillAmount = 1;
        button.enabled = clickable;
        if (!ReferenceEquals(onClick, null))
        {
            this.onClick = onClick;
        }
        else
        {
            this.onClick = null;
        }
    }



    private void Update()
    {
        PositionIndicator();
    }


    public void InvokeClick()
    {
        onClick?.Invoke();
    }


    private void PositionIndicator()
    {
        Vector3 targetSP = GameManager.Instance.CameraHandler.MainCamera.WorldToScreenPoint(target.transform.position);

        /* float angle = Mathf.Atan2(targetSP.y - midScreen.y, targetSP.x - midScreen.x);
         Vector3 posIndicator = new Vector3();

         posIndicator.x = Mathf.Cos(angle) * midScreen.x;
         posIndicator.y = Mathf.Sin(angle) * midScreen.y;
         posIndicator.z = 0f;

         RectTransform.localPosition = posIndicator;*/

        //trigo solution - works in 3d too. 
        
        Vector3 dirToTarget = (targetSP - midScreen).normalized;
        float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;

        rectTransform.anchoredPosition = dirToTarget * (((RectTransform)LevelManager.Instance.GameUi.transform).sizeDelta.magnitude * 0.5f);
        RectTransform.localPosition = new Vector2(Mathf.Clamp(rectTransform.localPosition.x, -midScreen.x, midScreen.x), Mathf.Clamp(rectTransform.localPosition.y, -midScreen.y, midScreen.y));
    }
}




