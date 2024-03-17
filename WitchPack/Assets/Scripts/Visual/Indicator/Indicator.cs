using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : UIElement
{
    [SerializeField] private Image artwork;
    [SerializeField] private Image circle;
    [SerializeField] private Button button;
    [SerializeField] private Transform pointer;

    private bool isPulsing;

    [SerializeField] private RectTransform artParentRectTransform;

    private Action onClick;
    private float time;
    private float counter;
    private Indicatable target;


    private Vector3 midScreen = new Vector3(Screen.width / 2, Screen.height / 2);
    private Vector2 referenceResolution;
    private Vector2 resolutionDifference;

    // pulse variables
    [SerializeField] private float pulsingSpeed;
    int speedDirection;
    [SerializeField] float minSizeValue;
    float maxSizeValue;

    [SerializeField] private Image pointerImage;
    [SerializeField] private List<Sprite> pointerSprites;

    public void InitIndicator(Indicatable target, Sprite artwork, float time = 0, bool clickable = false, Action onClick = null, bool isPulsing = false, 
                IndicatorPointerSpriteType indicatorPointerSprite = IndicatorPointerSpriteType.Default)
    {
        pointerImage.sprite = SetPointerImage(indicatorPointerSprite);

        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        this.isPulsing = isPulsing;
        counter = time;
        circle.fillAmount = 1;
        maxSizeValue = 1;
        speedDirection = 1;
        button.enabled = clickable;
        if (!ReferenceEquals(onClick, null))
        {
            this.onClick = onClick;
        }
        else
        {
            this.onClick = null;
        }
        referenceResolution = new Vector2(1920f, 1080f); 
        resolutionDifference = new Vector2(referenceResolution.x / Screen.width, referenceResolution.y / Screen.height);

    }

    private Sprite SetPointerImage(IndicatorPointerSpriteType indicatorPointerSprite)
    {
        int index = ((int)indicatorPointerSprite);
        return pointerSprites[index];
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
        IndicatorPulse();
    }


    public void InvokeClick()
    {
        onClick?.Invoke();
        gameObject.SetActive(false);        
    }


    private void PositionIndicator()
    {
        Vector3 targetScreenPoint = GameManager.Instance.CameraHandler.MainCamera.WorldToScreenPoint(target.transform.position);
        targetScreenPoint.x *= resolutionDifference.x;
        targetScreenPoint.y *= resolutionDifference.y;
        float clampedX = Mathf.Clamp(targetScreenPoint.x, 0, midScreen.x * 2);
        float clampedY = Mathf.Clamp(targetScreenPoint.y, 0, midScreen.y * 2);
        targetScreenPoint = new Vector3(clampedX, clampedY);
        targetScreenPoint -= (new Vector3(referenceResolution.x / 2f, referenceResolution.y / 2f)); // Why we do dis??
        rectTransform.localPosition = targetScreenPoint;
        /* float angle = Mathf.Atan2(targetSP.normalized.y, targetSP.normalized.y) * Mathf.Rad2Deg;
         pointer.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        //test

        Vector2 direction = new Vector2(0, 0); // will help calculate the angle at which we need to rotate our pointer
        Vector2 referenceVector = new Vector2(0, -1); // angle will be calculated in relation to this vector
        Vector3 axis = Vector3.forward; // the axis is used to flip the corner in case the target is to the right of the screen

        // y value of direction
        if (targetScreenPoint.y == -midScreen.y)
        {
            direction.y = 1;
        }
        else if (targetScreenPoint.y == midScreen.y)
        {
            direction.y = -1;
        }

        // x value of direction
        if (targetScreenPoint.x == -midScreen.x)
        {
            direction.x = 1;
        }
        else if (targetScreenPoint.x == midScreen.x)
        {
            direction.x = -1;
            axis = Vector3.back;
        }


        /// gets angle between our referenceVector and our calculated direction
        /// for future reference:
        /// 180 degrees = above screen
        /// 0 degrees = below screen
        /// 90 degrees = to the right of screen
        /// 270 degrees = to the left of screen
        float pointerAngle = Vector2.Angle(referenceVector, direction);
        rectTransform.localRotation = Quaternion.AngleAxis(pointerAngle, axis);

        artParentRectTransform.localEulerAngles = new Vector3(0, 0, -rectTransform.localEulerAngles.z);

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

    private void IndicatorPulse()
    {
        if(isPulsing == true)
        {            
            if(artParentRectTransform.localScale.x > maxSizeValue || artParentRectTransform.localScale.x < minSizeValue)
            {
                speedDirection *= -1;
            }
            float scaleChange = speedDirection * pulsingSpeed * GAME_TIME.GameDeltaTime;
            Vector3 scaleChangeVector = new Vector3(scaleChange, scaleChange, scaleChange);
            artParentRectTransform.localScale += scaleChangeVector;
        }
    }

    private void OnDisable()
    {
        rectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        //artwork.rectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        artParentRectTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        artParentRectTransform.localScale = new Vector3(1, 1, 1);
        speedDirection = 1;
        counter = time;
    }

}

public enum IndicatorPointerSpriteType
{
    Default,
    Cyan,
    Red
}




