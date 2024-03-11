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

    public void InitIndicator(Indicatable target, Sprite artwork, float time = 0, bool clickable = false, Action onClick = null)
    {
        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        counter = time;
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
        //decrease ring if time is set
        if (time != 0)
        {
            circle.fillAmount = counter / time;
            counter -= GAME_TIME.GameDeltaTime;
            if (counter <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        PositionIndicator();
    }


    public void InvokeClick()
    {
        onClick?.Invoke();
        gameObject.SetActive(false);        
    }


    private void PositionIndicator()
    {
        Vector3 targetSP = GameManager.Instance.CameraHandler.MainCamera.WorldToScreenPoint(target.transform.position);
        targetSP = new Vector3(Mathf.Clamp(targetSP.x, 0, midScreen.x * 2), Mathf.Clamp(targetSP.y, 0, midScreen.y * 2));
        targetSP -= midScreen;
        rectTransform.localPosition = targetSP;
        /* float angle = Mathf.Atan2(targetSP.normalized.y, targetSP.normalized.y) * Mathf.Rad2Deg;
         pointer.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        //test

        if (targetSP.y == -midScreen.y)
        {
            rectTransform.localRotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        else if (targetSP.y == midScreen.y)
        {
            rectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        else if (targetSP.x == -midScreen.x)
        {
            rectTransform.localRotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        else if (targetSP.x == midScreen.x)
        {
            rectTransform.localRotation = Quaternion.AngleAxis(270, Vector3.forward);
        }
        artwork.rectTransform.localEulerAngles = new Vector3(0, 0, -rectTransform.localEulerAngles.z);

        /* float angle = Mathf.Atan2(targetSP.y - midScreen.y, targetSP.x - midScreen.x);
         Vector3 posIndicator = new Vector3();

         posIndicator.x = Mathf.Cos(angle) * midScreen.x;
         posIndicator.y = Mathf.Sin(angle) * midScreen.y;
         posIndicator.z = 0f;

         RectTransform.localPosition = posIndicator;*/


        /*Vector3 dirToTarget = (targetSP - midScreen);
        rectTransform.localPosition = dirToTarget.normalized * (((RectTransform)LevelManager.Instance.GameUi.transform).sizeDelta.magnitude * 0.5f);
        RectTransform.localPosition = new Vector2(Mathf.Clamp(rectTransform.localPosition.x, -midScreen.x, midScreen.x), Mathf.Clamp(rectTransform.localPosition.y, -midScreen.y, midScreen.y));*/
    }

    private void OnDisable()
    {
        rectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        artwork.rectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

    }

}




