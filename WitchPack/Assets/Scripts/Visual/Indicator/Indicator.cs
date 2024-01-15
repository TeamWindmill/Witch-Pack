using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    private Vector3 halfScreenSize = new Vector3(Screen.width / 2, Screen.height / 2);

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


        float angle = Mathf.Atan2(targetSP.y - halfScreenSize.y, targetSP.x - halfScreenSize.x);
        Vector3 posIndicator = new Vector3();

        posIndicator.x = Mathf.Cos(angle) * halfScreenSize.x;
        posIndicator.y = Mathf.Sin(angle) * halfScreenSize.y;
        posIndicator.z = 0.0f;

        RectTransform.localPosition = posIndicator;


    }


}


